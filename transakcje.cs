//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Jednoslad2
{
    using System;
    using System.Collections.Generic;
    
    public partial class transakcje
    {
        public int id { get; set; }
        public Nullable<int> id_klienta { get; set; }
        public Nullable<int> id_motocykla { get; set; }
        public Nullable<System.DateTime> data_wypo { get; set; }
    
        public virtual klienci klienci { get; set; }
        public virtual motocykle motocykle { get; set; }
    }
}