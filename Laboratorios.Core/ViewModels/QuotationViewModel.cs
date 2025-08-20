using Laboratorios.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Laboratorios.Core.ViewModels
{
    public class Quotationvalue
    {
        public Quotation Quotations { get; set; }

        public Quotation BusinessPartners { get; set; }
    }
    public class QuotationViewModel
    {
        public QuotationViewModel()
        {
            this.value = new List<Quotationvalue>();
        }
        public List<Quotationvalue> value { get; set; }

        
    }
}
