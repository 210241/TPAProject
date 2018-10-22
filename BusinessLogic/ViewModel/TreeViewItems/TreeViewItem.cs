﻿using System.Collections.ObjectModel;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public abstract class TreeViewItem 
    {
        private bool _wasBuilt;
        private bool _isExpanded;
        public string Name { get; set; }
        public ItemTypeEnum ItemType { get; set; }
        public ObservableCollection<TreeViewItem> Children { get; set; }

        protected TreeViewItem(string name, ItemTypeEnum itemType)
        {
            Children = new ObservableCollection<TreeViewItem>() { null };
            this._wasBuilt = false;
            Name = name;
            ItemType = itemType;
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                if (_wasBuilt)
                    return;
                Children.Clear();
                BuildTreeView(Children);
                _wasBuilt = true;
            }
        }

        protected abstract void BuildTreeView(ObservableCollection<TreeViewItem> children);

    }
}