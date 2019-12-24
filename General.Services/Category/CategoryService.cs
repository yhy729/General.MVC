using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using General.Entities;
using General.Core.Data;

namespace General.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private IRepository<Entities.Category> _categoryRepository;
        public CategoryService(IRepository<Entities.Category> categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }
        public List<Entities.Category> GetAll()
        {
            return _categoryRepository.Table.ToList();
        }

        public void InitCategory(List<Entities.Category> categories)
        {
            var oldList = _categoryRepository.Table.ToList();
            oldList.ForEach(del=> {
                var item = categories.FirstOrDefault(x => x.SysResource == del.SysResource);
                if (item != null)
                {
                    var permissionList = del
                }
            });
        }
    }
}
