using DontLetMeExpire.Models;

namespace DontLetMeExpire.Services;

public static class DummyData
{

    public static List<StorageLocation> Locations { get; } =
    [
        new StorageLocation
        {
            Id = "f764f62e-d61c-48d4-ac2a-bcd8e1dce89e", Name = GetTranslation("Freezer", "Gefrierschrank"),
            Icon = "\uf166"
        },
        new StorageLocation
        {
            Id = "1ce2ed73-2353-496a-8381-82ac580dc16b", Name = GetTranslation("Refrigerator", "Kühlschrank"),
            Icon = "\ueb47"
        },
        new StorageLocation
        {
            Id = "fc01111d-1e47-406a-b695-04cf7bf4f521", Name = GetTranslation("Pantry", "Vorratsschrank"),
            Icon = "\uf86e"
        }
    ];

    public static List<Item> Items { get; } =
    [
        new Item
        {
            Id = "ea0b468a-220c-470f-aff6-3ebf7eadbfce",
            Name = GetTranslation("Leaf spinach 1lb (frozen)", "Blattspinat 500g (TK)"),
            ExpirationDate = DateTime.Today,
            StorageLocationId = Locations[0].Id,
            Amount = 1
        },
        new Item
        {
            Id = "a19d6623-f1e6-4b61-94ef-d7c620872ecd",
            Name = GetTranslation("Garden peas 1lb (frozen)", "Gartenerbsen 400g (TK)"),
            ExpirationDate = DateTime.Today.AddMonths(3).AddDays(5),
            StorageLocationId = Locations[0].Id,
            Amount = 1
        },
        new Item
        {
            Id = "29374ad8-3614-497c-891f-5202ebafcf6f",
            Name = GetTranslation("Vegetable stir fry 2lb (frozen)", "Gemüsepfanne 750g (TK)"),
            ExpirationDate = DateTime.Today.AddMonths(9).AddDays(3),
            StorageLocationId = Locations[0].Id,
            Amount = 1
        },
        new Item
        {
            Id = "d5c9b410-af26-4ebb-b074-1317b2399065",
            Name = GetTranslation("Mixed vegetables 1lb (frozen)", "Kaisergemüse 450g (TK)"),
            ExpirationDate = DateTime.Today.AddDays(4),
            StorageLocationId = Locations[0].Id,
            Amount = 2
        },
        new Item
        {
            Id = "1ce2ed73-2353-496a-8381-82ac580dc16b",
            Name = GetTranslation("Orange juice", "Orangensaft"),
            ExpirationDate = DateTime.Today.AddDays(3),
            StorageLocationId = Locations[1].Id,
            Amount = 5
        },
        new Item
        {
            Id = "12f0509b-567d-4983-ae81-fc5fb56e4117",
            Name = GetTranslation("Broccoli", "Brokkoli"),
            ExpirationDate = DateTime.Today.AddDays(6),
            StorageLocationId = Locations[1].Id,
            Amount = 2
        },
        new Item
        {
            Id = "a319b326-9fe3-4b70-ab26-bb66fe749c14",
            Name = GetTranslation("Vegetable broth", "Gemüsebrühe"),
            ExpirationDate = DateTime.Today.AddDays(-5),
            StorageLocationId = Locations[2].Id,
            Amount = 1
        },
        new Item
        {
            Id = "d1a7396a-9198-46ee-b541-f0e180911b7b",
            Name = GetTranslation("Rice", "Reis"),
            ExpirationDate = DateTime.Today.AddDays(-25),
            StorageLocationId = Locations[2].Id,
            Amount = 1
        }
    ];

    private static string GetTranslation(string english, string german)
    {
        return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "de" ? german : english;
    }
}