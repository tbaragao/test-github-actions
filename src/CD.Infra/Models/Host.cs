namespace CD.Infra.Models;

public record Host {
    public string Name { get; init; }
    public Guid Code { get; init; }
    public int Number { get; init; }
}

public record HostOptions
{
    public List<Host> Hosts { get; set; }
}