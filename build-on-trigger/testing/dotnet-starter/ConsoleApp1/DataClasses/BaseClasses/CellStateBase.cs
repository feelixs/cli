
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SqlOnAir.DotNet.Lib;

namespace SqlOnAir.DotNet.Lib.DataClasses.BaseClasses
{
    public class CellStateBase : SoAEntityBase
    {
        [Key]
        public string CellStateId { get; set; } = $"{Guid.NewGuid()}";

        public string? Name { get; set; }
        public string? Id { get; set; }
        public int? AlexesIndex { get; set; }
        public string? DefaultMark { get; set; }
        public string? Color { get; set; }
        public string? FontColor { get; set; }
        public string? Description { get; set; }
        public string? Cursor { get; set; }
        public string? SortOrder { get; set; }
        public string? CellPatternCells { get; set; }
        public string? CellPatterns { get; set; }
        public string? Code
        {
            get => null /* Unhandled formula: {DefaultMark} */; set { }
        }


        private ObservableCollection<Cell> _currentStateCells;

        public ObservableCollection<Cell> CurrentStateCells
        {
            get
            {
                if (_currentStateCells == null)
                {
                    if (Context == null)
                    {
                        _currentStateCells = new ObservableCollection<Cell>();
                    }
                    else
                    {
                        var items = Context.Cells.Where(x => x.CellStateId == this.CellStateId).ToList<Cell>();
                        _currentStateCells = new ObservableCollection<Cell>(items);
                        Context.AttachRange(items);
                    }
                    _currentStateCells.CollectionChanged += CurrentStateCells_CollectionChanged;
                }
                return _currentStateCells;
            }
            private set
            {
                if (_currentStateCells != null)
                {
                    _currentStateCells.CollectionChanged -= CurrentStateCells_CollectionChanged;
                }
                _currentStateCells = value;
                if (_currentStateCells != null)
                {
                    _currentStateCells.CollectionChanged += CurrentStateCells_CollectionChanged;
                }
            }
        }

        private void CurrentStateCells_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e?.NewItems != null)
            {
                foreach (var item in e.NewItems.Cast<Cell>())
                {
                    if (item.CellStates != this)
                    {
                        item.CellStates = this as CellStates;
                    }
                }
            }

            if (e?.OldItems != null)
            {
                foreach (var item in e.OldItems.Cast<Cell>())
                {
                    if (item.CellStates == this)
                    {
                        item.CellStates = null;
                    }
                }
            }
        }

        private ObservableCollection<Cell> _defaultStateCells;

        public ObservableCollection<Cell> DefaultStateCells
        {
            get
            {
                if (_defaultStateCells == null)
                {
                    if (Context == null)
                    {
                        _defaultStateCells = new ObservableCollection<Cell>();
                    }
                    else
                    {
                        var items = Context.Cells.Where(x => x.CellStateId == this.CellStateId).ToList<Cell>();
                        _defaultStateCells = new ObservableCollection<Cell>(items);
                        Context.AttachRange(items);
                    }
                    _defaultStateCells.CollectionChanged += DefaultStateCells_CollectionChanged;
                }
                return _defaultStateCells;
            }
            private set
            {
                if (_defaultStateCells != null)
                {
                    _defaultStateCells.CollectionChanged -= DefaultStateCells_CollectionChanged;
                }
                _defaultStateCells = value;
                if (_defaultStateCells != null)
                {
                    _defaultStateCells.CollectionChanged += DefaultStateCells_CollectionChanged;
                }
            }
        }

        private void DefaultStateCells_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e?.NewItems != null)
            {
                foreach (var item in e.NewItems.Cast<Cell>())
                {
                    if (item.CellStates != this)
                    {
                        item.CellStates = this as CellStates;
                    }
                }
            }

            if (e?.OldItems != null)
            {
                foreach (var item in e.OldItems.Cast<Cell>())
                {
                    if (item.CellStates == this)
                    {
                        item.CellStates = null;
                    }
                }
            }
        }


        protected override void LazyLoadProperties()
        {
            var currentStateCells = this.CurrentStateCells;
            var defaultStateCells = this.DefaultStateCells;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}