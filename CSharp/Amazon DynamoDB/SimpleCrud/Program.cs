var customTableRepository = new CustomTableRepository();

Console.WriteLine("** Method Get(1) ***");
Console.WriteLine((await customTableRepository.GetItemAsync("1")).Description);

Console.WriteLine("** Method GetAll() ***");
foreach (var item in await customTableRepository.GetAllAsync())
{
    Console.WriteLine(item.Description);
}

var customTable = new CustomTable()
{
    Id = Guid.NewGuid().ToString(),
    Description = "Test"
};

await customTableRepository.SaveAsync(customTable);
Console.WriteLine("{0} Inserted!", customTable.Id);

await customTableRepository.DeleteAsync(customTable.Id);
Console.WriteLine("{0} Removed!", customTable.Id);