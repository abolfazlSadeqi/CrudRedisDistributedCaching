using DAL.Contexts;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository;

class PersonRepository : GenericRepository<Person>, IPersonRepository
{
    public PersonRepository(RedisDistributedCachingContext context) : base(context) { }
}
