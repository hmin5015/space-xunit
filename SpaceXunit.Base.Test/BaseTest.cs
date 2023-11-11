namespace SpaceXunit.Base.Test;

public class BaseTest<T> : IDisposable where T : class
{
    private SpaceDbContext memoryDb;

    protected SpaceDbContext MemoryDb
    {
        get
        {
            if (memoryDb == null)
            {
                memoryDb = SpaceDbContext.CreateInMemoryDatabase();
            }

            return memoryDb;
        }
    }

    protected IFixture Fixture { get; private set; }

    protected Mock<IMapper> MapperMock { get; private set; }

    public BaseTest()
    {
        Fixture = new Fixture().Customize(new AutoMoqCustomization());
        
        /* 
         * The Freeze method in AutoFixture is used to create a frozen instance of a specific type T.
         * This means that whenever you request an instance of T from the Fixture, it will always return the same instance. 
         * This is particularly useful when you want to ensure that a specific object is shared across multiple parts of your test.
         */
        MapperMock = Fixture.Freeze<Mock<IMapper>>();
    }

    public void Dispose()
    {
        // Dispose of resources if needed
        memoryDb?.Dispose();

        // Reset expectations and recorded invocations on MapperMock
        MapperMock.Reset();
    }
}
