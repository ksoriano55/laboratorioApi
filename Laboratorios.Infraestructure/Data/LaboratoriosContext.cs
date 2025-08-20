using System;
using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Data
{
    public partial class LaboratoriosContext : DbContext
    {
        public LaboratoriosContext()
        {
        }

        public LaboratoriosContext(DbContextOptions<LaboratoriosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Matrix> Matrix { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Matrix_Area> Matrix_Area { get; set; }
        public virtual DbSet<Analisys> Analisys { get; set; }
        public virtual DbSet<Area_Analisys> Area_Analisys { get; set; }
        public virtual DbSet<Income> Income { get; set; }
        public virtual DbSet<Sample> Sample { get; set; }
        public virtual DbSet<IncomeDetail> IncomeDetail { get; set; }
        public virtual DbSet<SampleType> SampleType { get; set; }
        public virtual DbSet<TableNMP> TableNMP { get; set; }
        public virtual DbSet<TableNMPDetail> TableNMPDetail { get; set; }
        public virtual DbSet<IncomeViewModel> IncomeViewModel { get; set; }
        public virtual DbSet<Metodology> Metodology { get; set; }
        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<User_Roles> User_Roles { get; set; }
        public virtual DbSet<PermissionRol> PermissionRol { get; set; }
        public virtual DbSet<UnitOfMeasurement> UnitOfMeasurement { get; set; }
        public virtual DbSet<MicrobiologyResults> MicrobiologyResults { get; set; }
        public virtual DbSet<MicrobiologyResultsViewModel> MicrobiologyResultsViewModel { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<DocumentChangesLog> DocumentChangesLog { get; set; }
        public virtual DbSet<MasterList> MasterList { get; set; }
        public virtual DbSet<Vendors> Vendors { get; set; }
        public virtual DbSet<SamplesOutsourced> SamplesOutsourced { get; set; }
        public virtual DbSet<SamplesOutsourcedDetail> SamplesOutsourcedDetail { get; set; }
        public virtual DbSet<VendorAssessment> VendorAssessment { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<EquipmentCalibration> EquipmentCalibration { get; set; }
        public virtual DbSet<CVMProgram> CVMProgram { get; set; }
        public virtual DbSet<IncomeControlSupply> IncomeControlSupply { get; set; }
        public virtual DbSet<IncomeControlSupplyDetail> IncomeControlSupplyDetail { get; set; }
        public virtual DbSet<LogHistory> LogHistory { get; set; }
        public virtual DbSet<PhisycalChemistryResults> PhisycalChemistryResults { get; set; }
        public virtual DbSet<PhisycalChemistryResultsViewModel> PhisycalChemistryResultsViewModel { get; set; }
        public virtual DbSet<ConfigurationResults> ConfigurationResults { get; set; }
        public virtual DbSet<QualityControl> QualityControl { get; set; }
        public virtual DbSet<AnalisysControls> AnalisysControls { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<RecipeDetail> RecipeDetail { get; set; }
        public virtual DbSet<ProductionOrder> ProductionOrder { get; set; }
        public virtual DbSet<ProductionOrderDetail> ProductionOrderDetail { get; set; }
        public virtual DbSet<Container> Container { get; set; }
        public virtual DbSet<ConfigurationRecipes> ConfigurationRecipes { get; set; }
        public virtual DbSet<ReportHistory> ReportHistory { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<_Income> _Income { get; set; }
        public virtual DbSet<_ReportHistory> _ReportHistory { get; set; }
        public virtual DbSet<SubAnalisys> SubAnalisys { get; set; }
        public virtual DbSet<SamplesOutsourcedResult> SamplesOutsourcedResult { get; set; }
        public virtual DbSet<SamplesOutsourcedResultViewModel> SamplesOutsourcedResultViewModel { get; set; }
        public virtual DbSet<SamplesOutsourcedResultViewModelReport> SamplesOutsourcedResultViewModelReport { get; set; }
        public virtual DbSet<_ReportProductionOrderDetails> _ReportProductionOrderDetails { get; set; }
        public virtual DbSet<Package> Package { get; set; }
        public virtual DbSet<PackageDetail> PackageDetail { get; set; }
        public virtual DbSet<Shelves> Shelves { get; set; }
        public virtual DbSet<EquipmentCertificate> EquipmentCertificate { get; set; }

    }
}
