class test1
{
	[URL("dotnet:local:namespaceName.className#methodName")]
	Integer 
	test
	()
	;
}

class test2 : test1
{
	Integer 
	test
	;
}

class test3 : test2
{
	[URL("dotnet:local:namespaceName.className#methodName")]
	Integer 
	test
	()
	;
}