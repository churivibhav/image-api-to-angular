using System;
using System.Linq;
using LiteDB;

class DataService : IDisposable
{
    private LiteDatabase db;

    public void Initialize(string path) {
        db = new LiteDatabase(path);
    }

    public Image GetImage(Guid id) => 
        db.GetCollection<Image>("images")
        .Find(x => x.Id == id)
        .FirstOrDefault();

    public Guid CreateImage(Image image) {
        if (image == null) throw new ArgumentNullException(nameof(image));
        image.Id = Guid.NewGuid();
        db.GetCollection<Image>("images").Insert(image);
        return image.Id;
    }

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                db.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
    }
    #endregion
}