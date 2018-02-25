using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TwaWallet.Entity
{
    /// <summary>Extension metody ke třídě DbModelBuilder.</summary>
    public static class DbModelBuilderExtensions
    {
        /// <summary>
        /// Zaregistruje veřejné modelové třídy z předané assembly.
        /// Registrovány jsou veřejné třídy, avšak třídy s atributem NotMappedAttribute a ComplexTypeAttribute jsou ignorovány (nejsou registrovány).
        /// </summary>
        public static void RegisterModelFromAssembly(this DbModelBuilder modelBuilder, Assembly assembly)
        {
            Contract.Requires(modelBuilder != null, "modelBuilder != null");
            Contract.Requires(assembly != (Assembly)null, "assembly != null");
            foreach (Type type in ((IEnumerable<Type>)assembly.GetTypes()).Where<Type>((Func<Type, bool>)(assemblyType =>
            {
                if (assemblyType.IsPublic)
                    return assemblyType.IsClass;
                return false;
            })).ToList<Type>())
            {
                if (!type.GetCustomAttributes<NotMappedAttribute>().Any<NotMappedAttribute>() && !type.GetCustomAttributes<ComplexTypeAttribute>().Any<ComplexTypeAttribute>())
                    modelBuilder.RegisterEntityType(type);
            }
        }
    }
}
