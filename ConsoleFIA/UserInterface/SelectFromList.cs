using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFIA.UserInterface
{
    class SelectFromList<T> where T : class
    {
        public IList<T> Nodes { get; set; }
        private int selectedNodeIndex = 0;
        public int SelectedNodeIndex
        {
            get => selectedNodeIndex;
            set
            {
                if (value < 0)
                {
                    selectedNodeIndex = 0;
                }
                else if (value >= Nodes.Count)
                {
                    selectedNodeIndex = Nodes.Count - 1;
                }
                else
                {
                    selectedNodeIndex = value;
                }
            }
        }

        public T SelectedNode
        {
            get => Nodes.Count > 0
                ? Nodes[SelectedNodeIndex]
                : null;
            set => SelectedNodeIndex = Nodes.IndexOf(value);
        }

        public Menu Menu { get; }

        public SelectFromList(IList<T> nodes)
        {
            Menu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.UpArrow, "Вверх", () => SelectedNodeIndex--, true),
                new MenuAction(ConsoleKey.DownArrow, "Вниз", () => SelectedNodeIndex++, true),
            });
            Nodes = nodes;
        }
    }

    class SelectFromList<T, K>
    {
        public List<T> Nodes { get; set; }

        private Func<T, int> getNestedCount;

        private int count;
        private int Count
        {
            get 
            {
                count = 0;
                foreach (var node in Nodes)
                {
                    int nestedCount = getNestedCount(node);
                    count += (nestedCount == 0) ? 1 : nestedCount;
                }
                return count;
            }
        }

        private int selectedNodeIndex = 0;
        public int SelectedNodeIndex
        {
            get => selectedNodeIndex;
            set
            {
                if (value < 0)
                {
                    selectedNodeIndex = 0;
                }
                else if (value >= Count)
                {
                    selectedNodeIndex = Count - 1;
                }
                else
                {
                    selectedNodeIndex = value;
                }
            }
        }

        public T SelectedNode
        {
            get => Nodes[LinearStructOfNestedIndexes[selectedNodeIndex]];
        }

        private List<int> linearStructOfNestedIndexes = new List<int>();
        private List<int> LinearStructOfNestedIndexes
        {
            get
            {
                for (int i = 0; i < Nodes.Count; ++i)
                {
                    int nestedCount = getNestedCount(Nodes[i]);
                    nestedCount = nestedCount == 0 ? 1 : nestedCount;
                    for(int j = 0; j < nestedCount; ++j)
                    {
                        linearStructOfNestedIndexes.Add(i);
                    }
                }
                return linearStructOfNestedIndexes;
            }
        } 

        public Menu Menu { get; }

        public SelectFromList(List<T> nodes, Func<T, int> getNestedCount)
        {
            Menu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.UpArrow, "Вверх", () => SelectedNodeIndex--, true),
                new MenuAction(ConsoleKey.DownArrow, "Вниз", () => SelectedNodeIndex++, true),
            });
            Nodes = nodes;
            this.getNestedCount = getNestedCount;
        }
    }
}

