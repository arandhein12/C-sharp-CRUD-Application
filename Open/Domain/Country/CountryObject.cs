using Open.Data.Country;


namespace Open.Domain.Country
{
  public  class CountryObject{
      public readonly CountryDbRecord DbRecord;

      public CountryObject(CountryDbRecord r) {
          DbRecord = r ?? new CountryDbRecord();
      }
  }
}

