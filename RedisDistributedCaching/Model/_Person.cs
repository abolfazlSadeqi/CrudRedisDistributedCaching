namespace RedisDistributedCaching.Model
{



    public class _Person
    {
        public string Name { private set; get; }
        public int Id { private set; get; }
        public DateTime CreateDate { set; get; }
        public _Person(string _name, int _Id, DateTime createDate)
        {
            Name = _name;
            Id = _Id;
            CreateDate = createDate;
        }


    }
}
