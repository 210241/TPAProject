﻿using System;
using System.Collections.ObjectModel;
using BusinessLogic.Model;
using BusinessLogic.Reflection;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class MethodTreeItem : TreeViewItem
    {
        public MethodModel MethodModel { get; set; }
        public MethodTreeItem(MethodModel methodModel) : base(GetModifiers(methodModel) + methodModel.Name)
        {
            MethodModel = methodModel;
        }

        public static string GetModifiers(MethodModel model)
        {
            string type = null;
            type += model.Modifiers.Item1.ToString().ToLower() + " ";
            type += model.Modifiers.Item2 == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : String.Empty;
            type += model.Modifiers.Item3 == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : String.Empty;
            type += model.Modifiers.Item4 == VirtualEnum.Virtual ? VirtualEnum.Virtual.ToString().ToLower() + " " : String.Empty;
            return type;
        }

        protected override void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {

            if (MethodModel.GenericArguments != null)
            {
                foreach (TypeModel genericArgument in MethodModel.GenericArguments)
                {
                    children.Add(new TypeTreeItem(DictionaryTypeSingleton.Instance.Get(genericArgument.Name)));
                }
            }

            if (MethodModel.Parameters != null)
            {
                foreach (ParameterModel parameter in MethodModel.Parameters)
                {
                    children.Add(new ParameterTreeItem(parameter));
                }
            }

            if (MethodModel.ReturnType != null)
            {
                children.Add(new TypeTreeItem(DictionaryTypeSingleton.Instance.Get(MethodModel.ReturnType.Name)));
            }
        }
    }
}
