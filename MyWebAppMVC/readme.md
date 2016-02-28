# MyWebAppMVC
This is a template for ASP.NET MVC (Visual Studio 2013 C#) with SOLID architecture.

## Prepare project
* Create "Blank Solution", e.g. MyWebAppMVC
* Add Projects
	* MyWebAppMVC.WebUI as "ASP.NET Web Application", MVC
	* MyWebAppMVC.Contracts as "C# Class Library"
	* MyWebAppMVC.Models as "C# Class Library"
	* MyWebAppMVC.DAL as "C# Class Library"
	* delete default "Class1.cs"

## Define models (Code First approach)
* Create "Product.cs" in Models pj
	* make it public class
	* add ProductID property (public int ProductId { get; set; })
* add other classes and properties
* add references (System.ComponentModel.DataAnnotations) for [MaxLength(255)] 
```
public class Product
{
    public int ProductId { get; set; }
    [MaxLength(255)] 
    public string ProductName { get; set; }
}
```

## Get access to database in DAL
* Install "EntityFramework" using "TOOLS" -> Manage NuGet Packages for solution"
* Add references for Models pj
* Create "DataContext.cs" in DAL pj
	* make it public class
	* create constructor and DBset properties
```
public class DataContext : DbContext
{
    public DataContext():base("DefaultConnection"){}
    public DbSet<Product> Products { get; set; }
}
```

## Create Repository in DAL pj
* Create "Repositories" folder under the DAL pj
* Create "RepositoryBase.cs" in the Respositories folder
	* public abstract class RepositoryBase<TEntity> where TEntity : class
	*  SEE THE CODE
* Create "ProductRepository.cs" in the Respositories folder
	* it inherits ResporitoryBase
	* class ProductRepository : RepositoryBase<Product>

## Create Contracts (interfaces)
* Create "IRepositoryBase.cs"
	* open RepositoryBase
	* Right Click -> Refactor -> Extract Interface
	* make it public
* Move the interface into Contracts pj
	* create "Repositories" folder under the Contracts pj
	* move "IRepositoryBase.cs" in th in the Repositories folder.
	* delete the original one.
	* change the namespace of IRepositoryBase
	* add DAL references to Contracts 

## Create UI in WebUI pj
* Build the whole solution
* Add reference to the other projects
* Create controller
	* Right Click on Controllers -> add -> "new scaffolded item" -> "MVC 5 Controller with views, using Entity Framework"
	* Product (MyWebApp.Models), DataContext (MyWebApp.DAL)
* Change the way to access DB
	* use the following, instead of private DataContext db = new DataContext();,
	* IRepositoryBase<Product> products = new ProductRepository(new DataContext());
* Delete dependency on ProductRepository
	* install "Unity bootstrapper for ASP.NET MVC" using NuGet Packages (right click on WebUI pj)
	* add the following in "UnityConfig.cs" under "App_Start"
```
container.RegisterType<IRepositoryBase<Product>, ProductRepository>();
```
	* rewrite like the following in ProductsController
```
IRepositoryBase<Product> products;
public ProductsController(IRepositoryBase<Product> products)
{
    this.products = products;
}
```

## Check
* Go to the following URL:
	* http://localhost:4269/products
* or add link in menubar (in _Layout.cshtml)

## Prepare Database (optional)
* Open "Web.config" just under WebUI pj, and copy "connectionStrings" entry
* Open "App.config" in DAL pj, and pase it after configSections
* delete "AttachDbFilename" property and change the value of "Initial Catalog" easy to find
* copy and pase "connectionStrings" back to "Web.config"

## DB prepare
* NuGet Package Manager -> Package Manager Console
* Select DAL pj
* PM> Enable-Migrations
* PM> Add-Migration initial
* PM> Update-Database

## When model changed
* Create new repository and add it into DataContext, if new model
* PM> Add-Migration AddedModelXXX
* PM> Update-Database
* if error happens, disconnect Server Connection or restart Visual Studio

## Foreign key
```
public class Products    // parent
{
    [Key]
    public int ProductID { get; set; }
    [MaxLength(255)]
    public string Name { get; set; }
    [DisplayName("Unit Price")]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C}")]
    public decimal UnitPrice { get; set; }
    [MaxLength(255)]
    public string MakerName { get; set; }

    // child tables
    public virtual ICollection<Order> Orders { get; set; }
}
```
```
public class Order    // child
{
    [Key]
    public int OrderID { get; set; }
    [DisplayName("Product")]
    [ForeignKey("Product")]
    public int ProductID { get; set; }
    public virtual Product Product { get; set; }
    public int Quantity { get; set; }
    [DisplayName("Order Date")]
    [DataType(DataType.Date)]
    public DateTime OrderedDate { get; set; }
}
```

## Repositories
```
public abstract class RepositoryBase<TEntity> : MyWebAppMVC.Contracts.Repositories.IRepositoryBase<TEntity> where TEntity : class
{
    internal DataContext context;
    internal DbSet<TEntity> dbSet;

    public RepositoryBase(DataContext context)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }

    public virtual TEntity GetById(object id)
    {
        return dbSet.Find(id);
    }
    public virtual IQueryable<TEntity> GetAll()
    {
        return dbSet;
    }

    public IQueryable<TEntity> GetPaged(int top = 20, int skip = 0, object orderBy = null, object filter = null)
    {
        return null;
    }
    public virtual IQueryable<TEntity> GetAll(object filter)
    {
        return null;
    }
    public virtual TEntity GetFullObject(object id)
    {
        return null;
    }

    public virtual void Insert(TEntity entity)
    {
        dbSet.Add(entity);
    }
    public virtual void Update(TEntity entity)
    {
        dbSet.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }
    public virtual void Delete(TEntity entity)
    {
        if (context.Entry(entity).State == EntityState.Detached)
        {
            dbSet.Attach(entity);
        }
        dbSet.Remove(entity);
    }
    public virtual void Delete(object id)
    {
        TEntity entity = dbSet.Find(id);
        Delete(entity);
    }

    public virtual void Commit()
    {
        context.SaveChanges();
    }
    public virtual void Dispose()
    {
        context.Dispose();
    }
}
```

```
public class ProductRepository : RepositoryBase<Product>
{
    public ProductRepository(DataContext context)
        : base(context)
    {
        if (context == null)
            throw new ArgumentNullException();
    }

}
```
