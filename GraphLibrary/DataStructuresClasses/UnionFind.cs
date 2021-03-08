using System;

namespace GraphLibrary.DataStructuresClasses
{
    class UnionFind
    {
        private class Set
        {
            internal int ID;
            internal Set parent = null;
            internal int rank = 0;
        }

        Set[] tab;

        public UnionFind(int n)
        {
            if (n <= 0)
            {
                throw new ArgumentException();
            }

            tab = new Set[n];

            for (int i = 0; i < n; i++)
            {
                tab[i] = new Set() { ID = i };
            }
        }

        public int FindParent(int ID)
        {
            if (ID < 0 || ID >= tab.Length)
            {
                throw new ArgumentOutOfRangeException("ID");
            }

            if (null == tab[ID].parent)
            {
                return ID;
            }

            tab[ID].parent = tab[FindParent(tab[ID].parent.ID)];

            return tab[ID].parent.ID;
        }

        public void Union(int firstSet, int secondSet)
        {
            if (firstSet < 0 || firstSet >= tab.Length)
            {
                throw new ArgumentOutOfRangeException("newParentID");
            }

            if (secondSet < 0 || secondSet >= tab.Length)
            {
                throw new ArgumentOutOfRangeException("newChildID");
            }

            if (FindParent(firstSet) == FindParent(secondSet))
            {
                return;
            }

            firstSet = FindParent(firstSet);
            secondSet = FindParent(secondSet);

            if (tab[secondSet].rank <= tab[firstSet].rank)
            {
                tab[secondSet].parent = tab[firstSet];
                if (tab[secondSet].rank == tab[firstSet].rank)
                {
                    tab[firstSet].rank++;
                }
            }
            else
            {
                tab[firstSet].parent = tab[secondSet];
            }

        }
    }
}
