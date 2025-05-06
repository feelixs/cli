namespace GotDotNet.Exslt
{
    public class Pair
    {
        public int First { get; set; }
        public int Second { get; set; }

        public Pair(int startDoc, int endDoc)
        {
            this.First = startDoc;
            this.Second = endDoc;
        }

    }
}