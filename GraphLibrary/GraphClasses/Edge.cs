namespace GraphLibrary
{
    public class Edge
    {
        private readonly int from;
        private readonly int to;

        public int From { get => from; }
        public int To { get => to; }
        public double Weight { get; private set; }

        public Edge(int from, int to, double weight = 0)
        {
            this.from = from;
            this.to = to;
            Weight = weight;
        }

        public Edge(Edge e)
        {
            from = e.from;
            to = e.to;
            Weight = e.Weight;
        }

        public Edge()
        {
            from = -1;
            to = -1;
            Weight = 0;
        }

        public bool ModifyWeight(double modificationValue)
        {
            if (double.IsNaN(modificationValue))
            {
                return false;
            }
            if (double.IsInfinity(modificationValue))
            {
                Weight = modificationValue;
            }
            else
            {
                Weight += modificationValue;
            }
            return true;
        }

        public bool SetWeight(double newWeight)
        {
            Weight = newWeight;
            return true;
        }

        public bool Equals(Edge other)
        {
            return this.From == other.From && this.To == other.To && this.Weight == other.Weight;
        }
    }
}
