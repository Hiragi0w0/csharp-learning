using Class;

var client = new ItemApiClient();

// GET /items
Console.WriteLine("GET /items");
try
{
	var items = await client.GetAllItems();
	if (items is not null)
	{
		int size = items.Count();
		for (int i = 0; i < size; i++)
		{
			int id = items[i].id;

			Console.WriteLine($"{id.ToString()}, {items[i].name}, {(items[i].price).ToString()}");
		}
	}
}
catch (Exception ex)
{
	Console.WriteLine($"Error: {ex}");
}

// GET /items/{id}
Console.WriteLine("GET /items/{id}");


// GET /slow?ms=**
Console.WriteLine("GET /slow?ms=**");


// GET /error
Console.WriteLine("GET /error");