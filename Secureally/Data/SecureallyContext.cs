using Microsoft.EntityFrameworkCore;
using Secureally.Angular;
using Secureally.Models;
using Secureally.Models.Angular;

#nullable disable
namespace Secureally.Data
{
    public class SecureallyContext:DbContext
    {
        protected readonly IConfiguration configuration;
        public SecureallyContext(DbContextOptions<SecureallyContext> options) : base(options) { }
        public virtual DbSet<Society> society { get; set; }
        public virtual DbSet<Members> members { get; set; }
        public virtual DbSet<Vendors> vendors { get; set; }
        public virtual DbSet<Blocks> blocks { get; set; }
        public virtual DbSet<Cities> cities { get; set; }
        public virtual DbSet<GuestRecord> guestRecords { get; set; }
        public virtual DbSet<Guests> guests { get; set; }
        public virtual DbSet<Houses> houses { get; set; }
        public virtual DbSet<OTPRecord> otpRecord { get; set; }
        public virtual DbSet<QRCode> qrCode { get; set; }
        public virtual DbSet<QRCodeRecord> qrCodeRecord { get; set; }
        public virtual DbSet<Roles> roles { get; set; }
        public virtual DbSet<States> states { get; set; }
        public virtual DbSet<UserRole> userRole { get; set; }
        public virtual DbSet<Users> users { get; set; }
        public virtual DbSet<Vendors> Vendors { get; set; }
        public virtual DbSet<VendorsRecord> vendorsRecords { get; set; }
        public virtual DbSet<VisitorsRecord> visitorsRecords { get; set; }
        public virtual DbSet<Visitors> visitors { get; set; }
        public virtual DbSet<Workers> workers { get; set; }
        public virtual DbSet<WorkersRecord> workersRecord { get; set; }
        public virtual DbSet<Admins> admins { get; set; }
        public virtual DbSet<Register> register { get; set; }
        public virtual DbSet<Login> login { get; set; }






        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Society>().ToTable("SocietyMaster");
            modelBuilder.Entity<Admins>().ToTable("AdminsMaster");
            modelBuilder.Entity<Blocks>().ToTable("BlocksMaster");
            modelBuilder.Entity<Members>().ToTable("MembersMaster");
            modelBuilder.Entity<Vendors>().ToTable("VendorsMaster");
            modelBuilder.Entity<Cities>().ToTable("CitiesMaster");
            modelBuilder.Entity<GuestRecord>().ToTable("GuestRecord");
            modelBuilder.Entity<Guests>().ToTable("GuestsMaster");
            modelBuilder.Entity<Houses>().ToTable("HousesMaster");
            modelBuilder.Entity<OTPRecord>().ToTable("OTPRecord");
            modelBuilder.Entity<QRCode>().ToTable("QRcodeMaster");
            modelBuilder.Entity<QRCodeRecord>().ToTable("QRcodeRecord");
            modelBuilder.Entity<Roles>().ToTable("RolesRecord");
            modelBuilder.Entity<States>().ToTable("StatesRecord");
            modelBuilder.Entity<UserRole>().ToTable("UserRoleMaster");
            modelBuilder.Entity<Users>().ToTable("UsersMaster");
            modelBuilder.Entity<VendorsRecord>().ToTable("VendorsRecord");
            modelBuilder.Entity<VisitorsRecord>().ToTable("VisitorsRecord");
            modelBuilder.Entity<Visitors>().ToTable("VisitorsMaster");
            modelBuilder.Entity<Workers>().ToTable("WorkersMaster");
            modelBuilder.Entity<WorkersRecord>().ToTable("WorkersRecord");     
            modelBuilder.Entity<Register>().ToTable("RegisterAdmin");
            modelBuilder.Entity<Login>().ToTable("LoginAdmin");


        }
    
    }
}
