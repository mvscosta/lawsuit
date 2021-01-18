using System;
using System.Reflection;
using AutoFixture.Kernel;

namespace Mc2Tech.BaseTests
{
    public class IgnoreVirtualMembers : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if(context == null)
                throw new ArgumentException(nameof(context));

            var pi = request as PropertyInfo;
            if (pi == null)            
                return new NoSpecimen();                        

            if (pi.GetGetMethod().IsVirtual)
                return null;

            return new NoSpecimen();
        }
    }
}