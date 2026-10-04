using System;
using System.Text.RegularExpressions;

public record Employee(int iID, String strName, String strDepartment, int iJoinedYear)
{

}

public class EmployeeStore
{
	private readonly List<Employee> _employees = new();

	private bool IsValid(Employee data)
	{
		if ((data.iID < 0) || (data.iID > 100)) return false;
		if (string.IsNullOrWhiteSpace(data.strName)) return false;
		if (string.IsNullOrWhiteSpace(data.strDepartment)) return false;
		var now = System.DateTime.Now;
		if ((data.iJoinedYear < 1900) || (data.iJoinedYear > now.Year)) return false;

		return true;
	}

	public IEnumerable<Employee> GetAll()
	{
		return _employees;
	}

	public Employee? Get(int iID)
	{
		if ((iID < 0) || (iID > 100)) return null;

		int iSize = _employees.Count;
		for (int i=0; i<iSize; i++)
		{
			if (_employees[i].iID == iID)
			{
				return _employees[i];
			}
		}
		return null;
	}

	public bool Add(Employee data)
	{
		if (!IsValid(data)) return false;
		if (_employees.Any(el => el.iID == data.iID)) return false;

		_employees.Add(data);
		return true;
	}

	public bool Delete(int iID)
	{
		if ((iID < 0) || (iID > 100)) return false;

		int iSize = _employees.Count;
		for (int i=0; i<iSize; i++)
		{
			if (_employees[i].iID == iID)
			{
				_employees.RemoveAt(i);
				return true;
			}
		}
		return false;
	}

	public bool Update(Employee data)
	{
		if (!IsValid(data)) return false;

		int iSize = _employees.Count;
		for (int i=0; i<iSize; i++)
		{
			if (_employees[i].iID == data.iID)
			{
				_employees[i] = data;
				return true;
			}
		}
		return false;
	}
}