﻿using LevelEditorActions;
using Serializers;
using System.Collections.Generic;
using UnityEngine;

namespace EditorExpanded.Editor.Tools
{
    class MergeAction : SimplerAction
    {
        private ReferenceMap.Handle<GameObject> originalObjectHandle_;
        private ReferenceMap.Handle<GameObject> newObjectHandle_;
        private byte[] deletedObjectBytes_;
        private GroupAction groupAction_;

        private bool nothingtodo = false;

        public MergeAction(GameObject[] gameObjects)
        {
            ReferenceMap referenceMap = G.Sys.LevelEditor_.ReferenceMap_;

            if (gameObjects.Length > 0)
            {
                //Group.InitData data = new Group.InitData(Vector3.zero, Quaternion.identity, Vector3.one, new Bounds());
                //this.groupAction_ = new GroupAction(gameObjects.ToArray(), data);
                this.groupAction_ = Group.CreateGroupAction(gameObjects.ToArray(), gameObjects.ToArray()[0]);
                Group group = this.groupAction_.GroupObjects();
                this.originalObjectHandle_ = referenceMap.GetHandle<GameObject>(group.gameObject);
                this.deletedObjectBytes_ = BinarySerializer.SaveGameObjectToBytes(group.gameObject, Resource.LoadPrefab("Group"), true);
                this.groupAction_.Undo();
            }
            else
            {
                nothingtodo = true;
            }

            newObjectHandle_ = new ReferenceMap.Handle<GameObject>();

        }
        public override string Description_ => "Merges objects.";

        public override void Undo() => this.UnMergeObjects();

        public override void Redo() => this.MergeObjects();

        public void MergeObjects()
        {
            if (!nothingtodo)
            {

                Group objectGroup = groupAction_.GroupObjects();
                GameObject groupObject = objectGroup.gameObject;

                Vector3 groupPosition = groupObject.GetComponent<Transform>().localPosition;
                Quaternion groupRotation = groupObject.GetComponent<Transform>().localRotation;
                Vector3 groupSize = objectGroup.localBounds_.size;

                GameObject[] originalobjs = groupAction_.UngroupObjects();
                LevelEditor levelEditor = G.Sys.LevelEditor_;

                groupAction_.GroupObjects();
                levelEditor.DeleteGameObject(this.originalObjectHandle_.Get());
                List<LevelLayer> layersOfObjects = new List<LevelLayer>();
                LevelLayer origobjlayer = levelEditor.WorkingLevel_.GetLayerOfObject(originalobjs[0]);
                GameObject mergeObject = DuplicateObjectsAction.Duplicate(originalobjs[0]);
                levelEditor.AddGameObject(ref newObjectHandle_, mergeObject, origobjlayer);
                Vector3 mergeObjectPos = mergeObject.GetComponent<Transform>().position;
                Quaternion mergeObjectRot = mergeObject.GetComponent<Transform>().rotation;
                Vector3 mergeObjectScl = mergeObject.GetComponent<Transform>().localScale;
                //mergeObject.GetComponent<Transform>().rotation = Quaternion.identity;
                GroupAction groupAction_2 = Group.CreateGroupAction(new GameObject[] { mergeObject }, mergeObject);
                Group group = groupAction_2.GroupObjects();
                GameObject mergeObjectGroup = group.gameObject;
                mergeObjectGroup.GetComponent<Transform>().localRotation = mergeObjectRot;
                mergeObjectGroup.GetChild(0).GetComponent<Transform>().position = mergeObjectPos;
                mergeObjectGroup.GetChild(0).GetComponent<Transform>().rotation = mergeObjectRot;
                group.localBounds_ = Group.CalculateBoundsFromImmediateChildren(group);
                //Vector3 objsize = group.localBounds_.size;

                Vector3 mergeObjectSize = group.localBounds_.size;

                Vector3 newMergeObjectScale = new Vector3(groupSize.x / mergeObjectSize.x, groupSize.y / mergeObjectSize.y, groupSize.z / mergeObjectSize.z);

                mergeObjectGroup.GetComponent<Transform>().localPosition = groupPosition;
                mergeObjectGroup.GetComponent<Transform>().localRotation = groupRotation;
                mergeObjectGroup.GetComponent<Transform>().localScale = newMergeObjectScale;

                groupAction_2.UngroupObjects();
            }
        }
        public void UnMergeObjects()
        {
            if (!nothingtodo)
            {
                G.Sys.LevelEditor_.DeleteGameObject(this.newObjectHandle_.Get());

                LevelEditor levelEditor = G.Sys.LevelEditor_;

                GameObject newObj = Deserializer.LoadGameObjectFromBytes<BinaryDeserializer>(this.deletedObjectBytes_, levelEditor.ReferenceMap_.GetIDToObjectMap());
                if ((UnityEngine.Object)newObj == (UnityEngine.Object)null)
                {
                    Debug.LogError((object)"Error loading deleted object");
                }
                else
                {
                    levelEditor.AddGameObject(ref this.originalObjectHandle_, newObj, (LevelLayer)null);
                    G.Sys.LevelEditor_.SelectObject(newObj);
                    if (this.groupAction_ == null)
                        return;
                    this.groupAction_.groupObjectHandle_ = this.originalObjectHandle_;
                    this.groupAction_.UngroupObjects();
                }
            }
        }

    }
}
