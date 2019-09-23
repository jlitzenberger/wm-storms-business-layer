using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.STORMS.BusinessLayer.BusinessLogic.Interfaces
{
    public interface _IBusinessLogic<TObj, TEntity>
        where TObj : class
        where TEntity : class
    {
        TObj Get(TEntity entity);
        List<TObj> Get(IEnumerable<TEntity> entities);

        TObj MapEntityToObject(TEntity entity);
        IEnumerable<TObj> MapEntitiesToObjects(IEnumerable<TEntity> entities);

        TEntity MapObjectToEntity(TObj obj);
        IEnumerable<TEntity> MapObjectsToEntities(IEnumerable<TObj> objs);

        TEntity MapRootObjectToEntity(TObj obj, TEntity entity);
    }
   
}
