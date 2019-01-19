using System;
using System.Collections.Generic;
using System.Text;

namespace TB.Db
{
    public abstract class BaseEntity<T, K>
        where K : class, new()
        where T : BaseEntity<T, K>, new()
    {
        public int Id { get; set; }

        public static T GetEnt(K dto)
        {
            T ent = new T();
            ent.MapEntity(dto);
            return ent;
        }
        public abstract void MapEntity(K dto);
        public virtual K GetDto()
        {
            K dto = new K();
            return GetDto(dto);
        }
        public abstract K GetDto(K dto);
    }

}
