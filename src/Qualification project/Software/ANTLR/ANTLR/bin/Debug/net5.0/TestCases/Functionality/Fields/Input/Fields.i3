class test1
{
	[URL("dotnet:local:namespaceName.className#methodName")]
	Integer 
	test
	(Integer a, 
	Integer b, 
	Integer c)
	;
}

class test2 : test1
{
	[URL("dotnet:local:namespaceName.className#methodName")]
	Integer 
	test
	(Integer d, Integer e, Integer f)
	;
	
	[URL("dotnet:local:namespaceName.className#methodName")]
	Integer 
	test
	(Integer d, Integer e)
	;
	
	[URL("dotnet:local:namespaceName.className#methodName")]
	Integer 
	test
	(Integer d, 
	String e, 
	Integer f
	)
	;
}