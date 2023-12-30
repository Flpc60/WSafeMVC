namespace WSafe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataBase1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnnualPlanVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Cycle = c.String(maxLength: 2),
                        Activity = c.String(nullable: false, maxLength: 100),
                        Entregables = c.String(maxLength: 100),
                        Recursos = c.String(),
                        Responsable = c.String(),
                        Observation = c.String(maxLength: 200),
                        StateActivity = c.String(),
                        Programed = c.Short(nullable: false),
                        Executed = c.Short(nullable: false),
                        PorcentajeCumplimiento = c.String(),
                        Seguimients = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AuditedActions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuditID = c.Int(nullable: false),
                        NoConformance = c.String(nullable: false, maxLength: 100),
                        Cause = c.String(nullable: false, maxLength: 100),
                        CorrectiveAction = c.String(nullable: false, maxLength: 100),
                        WorkerID = c.Int(nullable: false),
                        ExecutionDate = c.DateTime(nullable: false),
                        AuditState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Audits", t => t.AuditID, cascadeDelete: true)
                .Index(t => t.AuditID);
            
            CreateTable(
                "dbo.AuditedCreateVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuditDate = c.DateTime(nullable: false),
                        AuditerID = c.Int(nullable: false),
                        AuditProcess = c.Int(nullable: false),
                        WorkerID = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AuditedResults",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuditID = c.Int(nullable: false),
                        AuditItemID = c.Int(nullable: false),
                        Result = c.Int(nullable: false),
                        Process = c.Int(nullable: false),
                        AuditChapter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AuditItems", t => t.AuditItemID, cascadeDelete: true)
                .ForeignKey("dbo.Audits", t => t.AuditID, cascadeDelete: true)
                .Index(t => t.AuditID)
                .Index(t => t.AuditItemID);
            
            CreateTable(
                "dbo.AuditItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 400),
                        NormaID = c.Int(nullable: false),
                        AuditChapter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Auditers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Document = c.String(nullable: false, maxLength: 20),
                        RegisterNumber = c.String(nullable: false, maxLength: 20),
                        Idoneidad = c.String(nullable: false, maxLength: 100),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Audits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuditDate = c.DateTime(nullable: false),
                        Process = c.Int(nullable: false),
                        WorkerID = c.Int(nullable: false),
                        AuditDocument = c.String(maxLength: 100),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        AuditerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SigueAudits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuditID = c.Int(nullable: false),
                        SeguimientDate = c.DateTime(nullable: false),
                        SupervisorReport = c.String(nullable: false, maxLength: 200),
                        WorkerReport = c.String(nullable: false, maxLength: 200),
                        SSTReport = c.String(nullable: false, maxLength: 200),
                        Observations = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Audits", t => t.AuditID, cascadeDelete: true)
                .Index(t => t.AuditID);
            
            CreateTable(
                "dbo.CreatePlanActivityVMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NormaID = c.Int(nullable: false),
                        Activity = c.String(nullable: false, maxLength: 100),
                        Entregables = c.String(maxLength: 200),
                        Financieros = c.Boolean(nullable: false),
                        Administrativos = c.Boolean(nullable: false),
                        Tecnicos = c.Boolean(nullable: false),
                        Humanos = c.Boolean(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        Observation = c.String(maxLength: 100),
                        InitialDate = c.DateTime(nullable: false),
                        FechaFinal = c.DateTime(nullable: false),
                        Programed = c.Short(nullable: false),
                        ActivityFrequency = c.Int(nullable: false),
                        StateActivity = c.Int(nullable: false),
                        ActionCategory = c.Int(nullable: false),
                        StateCronogram = c.Int(nullable: false),
                        EvaluationID = c.Int(nullable: false),
                        AuditID = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        FileName = c.String(maxLength: 200),
                        DateSigue = c.DateTime(nullable: false),
                        Executed = c.Short(nullable: false),
                        TextDateSigue = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SiguePlanAnuals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateSigue = c.DateTime(nullable: false),
                        TrabajadorID = c.Int(nullable: false),
                        StateActivity = c.Int(nullable: false),
                        StateCronogram = c.Int(nullable: false),
                        Executed = c.Short(nullable: false),
                        Programed = c.Short(nullable: false),
                        Observation = c.String(maxLength: 200),
                        ActionCategory = c.Int(nullable: false),
                        FileName = c.String(maxLength: 200),
                        PlanActivityID = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PlanActivities", t => t.PlanActivityID, cascadeDelete: true)
                .Index(t => t.PlanActivityID);
            
            AddColumn("dbo.PlanActions", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActions", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActions", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.Aplicacions", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.AplicacionVMs", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.AplicacionVMs", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.AplicacionVMs", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivityVMs", "AuditID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivityVMs", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivityVMs", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivityVMs", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Organizations", "StandardAudits", c => c.Short(nullable: false));
            AddColumn("dbo.Organizations", "StandardAnnualPlan", c => c.Short(nullable: false));
            AddColumn("dbo.PlanActivities", "Entregables", c => c.String(maxLength: 200));
            AddColumn("dbo.PlanActivities", "StateActivity", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivities", "AuditID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivities", "OrganizationID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivities", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivities", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivities", "StateCronogram", c => c.Int(nullable: false));
            AddColumn("dbo.PlanActivities", "Programed", c => c.Short(nullable: false));
            AddColumn("dbo.PlanActivities", "InitialDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PlanActivities", "ActivityFrequency", c => c.Int(nullable: false));
            AddColumn("dbo.RoleUserVMs", "RegisterDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trabajadors", "FreeTime", c => c.Int(nullable: false));
            AddColumn("dbo.Trabajadors", "IngresoCategory", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "RegisterDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserViewModels", "RegisterDate", c => c.String(maxLength: 10));
            AddColumn("dbo.WorkersVMs", "FreeTime", c => c.Int(nullable: false));
            AddColumn("dbo.WorkersVMs", "IngresoCategory", c => c.Int(nullable: false));
            AlterColumn("dbo.MinimalsStandardsVMs", "ClaseRiesgo", c => c.Int(nullable: false));
            AlterColumn("dbo.Organizations", "ClaseRiesgo", c => c.Int(nullable: false));
            AlterColumn("dbo.Organizations", "NumeroTrabajadores", c => c.Short(nullable: false));
            AlterColumn("dbo.Organizations", "Year", c => c.Short(nullable: false));
            AlterColumn("dbo.Organizations", "StandardSocioDemographic", c => c.Short(nullable: false));
            AlterColumn("dbo.Organizations", "StandardUnsafeacts", c => c.Short(nullable: false));
            AlterColumn("dbo.Organizations", "StandardIndicators", c => c.Short(nullable: false));
            AlterColumn("dbo.PlanActivities", "Activity", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Trabajadors", "DiasPago", c => c.Short(nullable: false));
            AlterColumn("dbo.Trabajadors", "Profesion", c => c.String(maxLength: 50));
            AlterColumn("dbo.Trabajadors", "Conyuge", c => c.String(maxLength: 50));
            AlterColumn("dbo.Trabajadors", "NumberHijos", c => c.Short(nullable: false));
            AlterColumn("dbo.Trabajadors", "Enfermedad", c => c.String(maxLength: 50));
            AlterColumn("dbo.Trabajadors", "Tratamiento", c => c.String(maxLength: 50));
            AlterColumn("dbo.Trabajadors", "SpecialRecomendations", c => c.String(maxLength: 50));
            AlterColumn("dbo.WorkersVMs", "Profesion", c => c.String(maxLength: 50));
            AlterColumn("dbo.WorkersVMs", "Conyuge", c => c.String(maxLength: 50));
            AlterColumn("dbo.WorkersVMs", "NumberHijos", c => c.Short(nullable: false));
            AlterColumn("dbo.WorkersVMs", "Enfermedad", c => c.String(maxLength: 50));
            AlterColumn("dbo.WorkersVMs", "Tratamiento", c => c.String(maxLength: 50));
            AlterColumn("dbo.WorkersVMs", "SpecialRecomendations", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SiguePlanAnuals", "PlanActivityID", "dbo.PlanActivities");
            DropForeignKey("dbo.SigueAudits", "AuditID", "dbo.Audits");
            DropForeignKey("dbo.AuditedResults", "AuditID", "dbo.Audits");
            DropForeignKey("dbo.AuditedActions", "AuditID", "dbo.Audits");
            DropForeignKey("dbo.AuditedResults", "AuditItemID", "dbo.AuditItems");
            DropIndex("dbo.SiguePlanAnuals", new[] { "PlanActivityID" });
            DropIndex("dbo.SigueAudits", new[] { "AuditID" });
            DropIndex("dbo.AuditedResults", new[] { "AuditItemID" });
            DropIndex("dbo.AuditedResults", new[] { "AuditID" });
            DropIndex("dbo.AuditedActions", new[] { "AuditID" });
            AlterColumn("dbo.WorkersVMs", "SpecialRecomendations", c => c.String());
            AlterColumn("dbo.WorkersVMs", "Tratamiento", c => c.String());
            AlterColumn("dbo.WorkersVMs", "Enfermedad", c => c.String());
            AlterColumn("dbo.WorkersVMs", "NumberHijos", c => c.Int(nullable: false));
            AlterColumn("dbo.WorkersVMs", "Conyuge", c => c.String());
            AlterColumn("dbo.WorkersVMs", "Profesion", c => c.String());
            AlterColumn("dbo.Trabajadors", "SpecialRecomendations", c => c.String());
            AlterColumn("dbo.Trabajadors", "Tratamiento", c => c.String());
            AlterColumn("dbo.Trabajadors", "Enfermedad", c => c.String());
            AlterColumn("dbo.Trabajadors", "NumberHijos", c => c.Int(nullable: false));
            AlterColumn("dbo.Trabajadors", "Conyuge", c => c.String());
            AlterColumn("dbo.Trabajadors", "Profesion", c => c.String());
            AlterColumn("dbo.Trabajadors", "DiasPago", c => c.Int(nullable: false));
            AlterColumn("dbo.PlanActivities", "Activity", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Organizations", "StandardIndicators", c => c.Int(nullable: false));
            AlterColumn("dbo.Organizations", "StandardUnsafeacts", c => c.Int(nullable: false));
            AlterColumn("dbo.Organizations", "StandardSocioDemographic", c => c.Int(nullable: false));
            AlterColumn("dbo.Organizations", "Year", c => c.String(nullable: false, maxLength: 4));
            AlterColumn("dbo.Organizations", "NumeroTrabajadores", c => c.Int(nullable: false));
            AlterColumn("dbo.Organizations", "ClaseRiesgo", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.MinimalsStandardsVMs", "ClaseRiesgo", c => c.String());
            DropColumn("dbo.WorkersVMs", "IngresoCategory");
            DropColumn("dbo.WorkersVMs", "FreeTime");
            DropColumn("dbo.UserViewModels", "RegisterDate");
            DropColumn("dbo.Users", "RegisterDate");
            DropColumn("dbo.Trabajadors", "IngresoCategory");
            DropColumn("dbo.Trabajadors", "FreeTime");
            DropColumn("dbo.RoleUserVMs", "RegisterDate");
            DropColumn("dbo.PlanActivities", "ActivityFrequency");
            DropColumn("dbo.PlanActivities", "InitialDate");
            DropColumn("dbo.PlanActivities", "Programed");
            DropColumn("dbo.PlanActivities", "StateCronogram");
            DropColumn("dbo.PlanActivities", "UserID");
            DropColumn("dbo.PlanActivities", "ClientID");
            DropColumn("dbo.PlanActivities", "OrganizationID");
            DropColumn("dbo.PlanActivities", "AuditID");
            DropColumn("dbo.PlanActivities", "StateActivity");
            DropColumn("dbo.PlanActivities", "Entregables");
            DropColumn("dbo.Organizations", "StandardAnnualPlan");
            DropColumn("dbo.Organizations", "StandardAudits");
            DropColumn("dbo.PlanActivityVMs", "UserID");
            DropColumn("dbo.PlanActivityVMs", "ClientID");
            DropColumn("dbo.PlanActivityVMs", "OrganizationID");
            DropColumn("dbo.PlanActivityVMs", "AuditID");
            DropColumn("dbo.AplicacionVMs", "UserID");
            DropColumn("dbo.AplicacionVMs", "ClientID");
            DropColumn("dbo.AplicacionVMs", "OrganizationID");
            DropColumn("dbo.Aplicacions", "UserID");
            DropColumn("dbo.Aplicacions", "ClientID");
            DropColumn("dbo.Aplicacions", "OrganizationID");
            DropColumn("dbo.PlanActions", "UserID");
            DropColumn("dbo.PlanActions", "ClientID");
            DropColumn("dbo.PlanActions", "OrganizationID");
            DropTable("dbo.SiguePlanAnuals");
            DropTable("dbo.CreatePlanActivityVMs");
            DropTable("dbo.SigueAudits");
            DropTable("dbo.Audits");
            DropTable("dbo.Auditers");
            DropTable("dbo.AuditItems");
            DropTable("dbo.AuditedResults");
            DropTable("dbo.AuditedCreateVMs");
            DropTable("dbo.AuditedActions");
            DropTable("dbo.AnnualPlanVMs");
        }
    }
}
