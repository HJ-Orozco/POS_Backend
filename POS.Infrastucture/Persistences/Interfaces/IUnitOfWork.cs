using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastucture.Persistences.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        //declaracion o matricula de nuestra interfaces a nivel de repository

        ICategoryRepository Category { get; }


        void SaveChanges();

        Task SaveChangesAsync();
    }
}
