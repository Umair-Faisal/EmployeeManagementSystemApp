using System.Text;

namespace Backend.Collections
{
    public class CustomCollection<T> : List<T> where T : class
    {

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this)
            {
                sb.Append(item.ToString());
                sb.Append(", ");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            return sb.ToString();
        }


        public CustomCollection()
        {

        }
        public CustomCollection(IEnumerable<T> list)
        {
            foreach (var item in list)
                this.Add(item);
        }


        public void FromEnum(IEnumerable<T> list)
        {
            this.Clear();
            foreach (var item in list)
                this.Add(item);
        }
    }

}
