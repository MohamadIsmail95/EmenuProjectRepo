using EmenuDAL.Model.ViewModel;
using EmenuDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attribute = EmenuDAL.Model.Attribute;

namespace EmenuDAL.IRepository
{
    public interface IAttributeRepository: IRepositoryBase<Attribute, AttributeViewModel, int>
    {
    }
}
