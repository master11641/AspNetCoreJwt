using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core {
    // public class BloggingContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    public class BloggingContext : DbContext {
        public BloggingContext (DbContextOptions<BloggingContext> options) : base (options) { }
        public BloggingContext () { }
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<TordergoodsAttributeValues> TordergoodsAttributeValues { get; set; }
        public DbSet<Tstore> Tstores { get; set; }
        public DbSet<Torder> Torders { get; set; }
        public DbSet<TgoodsPrice> TgoodsPrices { get; set; }
        public DbSet<TgoodsImage> TgoodsImages { get; set; }
        public DbSet<TgoodsAttributeValue> TgoodsAttributeValues { get; set; }
        public DbSet<TgoodsAttribute> TgoodsAttributes { get; set; }
        public DbSet<Tgoods> Tgoodses { get; set; }
        public DbSet<TcategoryStore> TcategoryStores { get; set; }
        public DbSet<TcategoryGoods> TcategoryGoodss { get; set; }
        public DbSet<Province> Provinces { get; set; }
         public DbSet<Tfactor> Tfactors { get; set; }
        public DbSet<City> Cities { get; set; }
         public DbSet<Address> Addresses { get; set; }
        public DbSet<Adver> Advers { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder options) => options.UseSqlServer (@"Data Source=158.58.187.220\MSSQLSERVER2017;Initial Catalog=persia31_siadune;Integrated Security = False;User ID=persia31;pwd=6W34dYzbd3;");
        //protected override void OnConfiguring (DbContextOptionsBuilder options) => options.UseSqlServer (@"Data Source=(local);Initial Catalog=Blog;Integrated Security = true;MultipleActiveResultSets=true");
        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<TordergoodsAttributeValues> ().HasKey (sc => new { sc.TorderId, sc.GoodsAttributeValueId });
            modelBuilder.Entity<TcategoryGoods> ().HasOne (x => x.Parent).WithMany (x => x.Children).HasForeignKey (x => x.ParentID);
            modelBuilder.Entity<TcategoryStore> ().HasOne (x => x.Parent).WithMany (x => x.Children).HasForeignKey (x => x.ParentID);
            base.OnModelCreating (modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

        }

    }
    public class User {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
    public class Address {
        public int Id { get; set; }

        [ForeignKey ("UserId")]
        public virtual User User { get; set; }

        public int UserId { get; set; }

        [Display (Name = "نام و نام خانوادگی تحویل گیرنده")]
        [Required (ErrorMessage = "تکمیل نام و نام خانوادگی تحویل گیرنده الزامی است .")]
        public string NameDeliver { get; set; }

        [Display (Name = "شماره همراه تحویل گیرنده")]
        [UIHint ("MobileNumber")]
        [Required (ErrorMessage = "تکمیل فیلد شماره همراه تحویل گیرنده الزامی است .")]
        public string MobileDeliver { get; set; }

        [Display (Name = "شماره تلفن ثابت تحویل گیرنده")]
        public string PhoneDeliver { get; set; }

        [Display (Name = "شهر محل سکونت تحویل گیرنده")]
        [ForeignKey ("CityId")]
        public City City { get; set; }

        [Display (Name = "شهر محل سکونت تحویل گیرنده")]
        // [Required(ErrorMessage = "انتخاب شهر الزامی است .")]
        public int? CityId { get; set; }

        public string Sector { get; set; }

        // [Required(ErrorMessage = "تکمیل فیلد آدرس الزامی است .")]
        [Display (Name = "آدرس کامل")]
        [UIHint ("MultiLineText")]
        public string FullAddress { get; set; }

        [Display (Name = "کد پستی")]
        public string PostalCode { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
    public class City {
        public int Id { get; set; }

        [Required (ErrorMessage = "تکمیل فیلد نام الزامی است .")]
        [Display (Name = "نام شهرستان")]
        public string Name { get; set; }

        [Display (Name = "مرکز شهرستان")]
        public string Center { get; set; }

        [Display (Name = "کد شهرستان")]
        public int Code { get; set; }

        [Display (Name = "سال انتزاع")]
        public int DateCity { get; set; }

        [UIHint ("GridForeignKey")]
        [Display (Name = "استان مربوطه")]
        public int ProvinceId { get; set; }

        [ForeignKey ("ProvinceId")]
        public virtual Province Province { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
    public class Province {
        public int Id { get; set; }

        [Required (ErrorMessage = "تکمیل فیلد نام الزامی است .")]
        [Display (Name = "نام استان")]
        public string Name { get; set; }

        [Required (ErrorMessage = "تکمیل فیلد مرکز استان الزامی است .")]
        [Display (Name = "مرکز استان")]
        public string Center { get; set; }

        [Display (Name = "سال تفکیک")]
        public int ProvinceDate { get; set; }

        [Display (Name = "کد استان")]
        public int Code { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
    public class TordergoodsAttributeValues {
        public int TorderId { get; set; }
        public Torder Torder { get; set; }

        public int GoodsAttributeValueId { get; set; }
        public TgoodsAttributeValue TgoodsAttributeValue { get; set; }
    }

    public class Blog {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; } = new List<Post> ();
    }

    public class Post {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

    public class Tstore {
        public Tstore () {
            Tgoodses = new List<Tgoods> ();
        }
        public int Id { get; set; }
        //[ForeignKey("CityId")]
        // public virtual City City { get; set; }

        public int CityId { get; set; }
        public string Title { get; set; }

        [ForeignKey ("TcategoryStoreId")]
        public virtual TcategoryStore TcategoryStore { get; set; }

        public int TcategoryStoreId { get; set; }
        public string Address { get; set; }
        public bool IsFreeDeliveryExist { get; set; }
        public string ImageUrl { get; set; }
        public string LogoImageUrl { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsDeleted { get; set; }
        public List<Tgoods> Tgoodses { get; set; }
        public string UserOwner { get; set; }
    }
    public enum TorderStatus {
        Registered,
        InProgress,
        Posted,
        Delivered,
    }

    public class Torder {
        public Torder () {
            //  TordergoodsAttributeValues = new List<TordergoodsAttributeValues>();
        }
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow.AddHours (3.5);

        [ForeignKey ("TgoodsId")]
        public virtual Tgoods Tgoods { get; set; }

        public int TgoodsId { get; set; }
        public string UserName { get; set; }
        public int Count { get; set; }
        public virtual List<TgoodsAttributeValue> TgoodsAttributeValues { get; set; }
        public int? TfactorId { get; set; }
        public TorderStatus TorderStatus { get; set; }

        [ForeignKey("TfactorId")]
        public Tfactor Tfactor { get; set; }

        public int? AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }
    }
  public class Tfactor 
    {
         public int Id { get; set; }
        public ICollection<Torder> Torders { get; set; }
    }
    public class TgoodsPrice {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public int TgoodsId { get; set; }

        [ForeignKey ("TgoodsId")]
        public virtual Tgoods Tgoods { get; set; }
    }
    public class TgoodsImage {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey ("TgoodsId")]
        public Tgoods Tgoods { get; set; }

        public int TgoodsId { get; set; }
    }
    public class TgoodsAttributeValue {
        public TgoodsAttributeValue () {
            //  Torders = new List<Torder>();
        }
        public int Id { get; set; }
        public string Value { get; set; }

        public string Caption { get; set; }
        public int TgoodsAttributeId { get; set; }

        [ForeignKey ("TgoodsAttributeId")]
        public virtual TgoodsAttribute TgoodsAttribute { get; set; }

        public virtual List<TordergoodsAttributeValues> TordergoodsAttributeValues { get; set; }
    }
    public enum TattributeType {
        Color,
        Image,
        ClotheSize,
        InputText,
        Other,
    }

    public class TgoodsAttribute {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Required { get; set; }
        public int? GoodsCount { get; set; }
        public int DisplayOrder { get; set; }
        public int TgoodsId { get; set; }

        [ForeignKey ("TgoodsId")]
        public virtual Tgoods Tgoods { get; set; }

        public virtual List<TgoodsAttributeValue> TgoodsAttributeValues { get; set; }
        public TattributeType TattributeType { get; set; }
    }
    public enum GoodsStatus {
        [Display (Name = "موجود")]
        Available,

        [Display (Name = "ناموجود")]
        NotAvailable,

        [Display (Name = "به زودی")]
        ComingSoon
    }

    public enum GoodsType {
        [Display (Name = "قابل فروش")]
        Consumer,

        [Display (Name = "خدماتی")]
        Service,
    }

    public class Tgoods {
        public Tgoods () {
            Torders = new List<Torder> ();
            TgoodsImages = new List<TgoodsImage> ();
            TgoodsAttributes = new List<TgoodsAttribute> ();
        }
        public int Id { get; set; }
        public string Title { get; set; }

        [ForeignKey ("TstoreId")]
        public virtual Tstore Tstore { get; set; }

        public int TstoreId { get; set; }

        [ForeignKey ("TcategoryGoodsId")]
        public virtual TcategoryGoods TcategoryGoods { get; set; }

        public string Abstract { get; set; }
        public string Description { get; set; }
        public int TcategoryGoodsId { get; set; }
        public bool IsFreeDeliveryExist { get; set; }
        public GoodsStatus GoodsStatus { get; set; }
        public GoodsType GoodsType { get; set; }
        public bool IsConfirm { get; set; }
        public int? Count { get; set; }
        public List<Torder> Torders { get; set; }
        public List<TgoodsImage> TgoodsImages { get; set; }
        public List<TgoodsPrice> TgoodsPrices { get; set; }
        public List<TgoodsAttribute> TgoodsAttributes { get; set; }
    }
    public class TcategoryStore {
        public TcategoryStore () {
            Children = new List<TcategoryStore> ();
            Tstores = new List<Tstore> ();
        }
        public int Id { get; set; }
        public string Title { get; set; }

        [Display (Name = "گروه والد")]
        public int? ParentID { get; set; }

        public virtual TcategoryStore Parent { get; set; }
        public List<TcategoryStore> Children { get; set; }
        public List<Tstore> Tstores { get; set; }
    }

    // public class TcategoryStoreConfig : EntityTypeConfiguration<TcategoryStore>
    // {
    //     public TcategoryStoreConfig()
    //     {
    //         this.HasOptional(x => x.Parent)
    //             .WithMany(x => x.Children)
    //             .HasForeignKey(x => x.ParentID)
    //             .WillCascadeOnDelete(false);
    //     }
    // }
    public class TcategoryGoods {
        public TcategoryGoods () {
            Children = new List<TcategoryGoods> ();
            Tgoodses = new List<Tgoods> ();
        }
        public int Id { get; set; }
        public string Title { get; set; }

        [Display (Name = "گروه والد")]
        public int? ParentID { get; set; }

        public virtual TcategoryGoods Parent { get; set; }
        public List<TcategoryGoods> Children { get; set; }
        public List<Tgoods> Tgoodses { get; set; }
    }

    public class Adver {
        public int Id { get; set; }

        [Display (Name = "عنوان انگلیسی")]
        [Required (ErrorMessage = "تکمیل فیلد عنوان  ضروری است .")]
        public string Title { get; set; }

        [Required (ErrorMessage = "تکمیل فیلد توضیحات ضروری است .")]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
        public string UserName { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.UtcNow.AddHours (3.5);
        //public virtual Form Form { get; set; }
        //[Display(Name = "انتخاب فرم")]
        //public int FormId { get; set; }
        //public virtual ICollection<Value> Values { get; set; }
    }
}