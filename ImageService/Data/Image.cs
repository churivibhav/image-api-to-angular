using System;

class Image {
    public Guid Id { get; set; }
    public string Filename { get; set; }
    public string Filetype { get; set; }
    public byte[] Data { get; set; }
}