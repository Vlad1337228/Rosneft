//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExampleApp.MyDbContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class Car
    {
        public string VIN { get; set; }
        public string gos_number { get; set; }
        public Nullable<byte> number_of_doors { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
    
        public virtual Group Group { get; set; }
    }
}
