class test1
{
	[URL("dotnet:local:namespaceName.className#methodName")]
	Integer 
	test
	(Integer a)
	;
}

class test2 : test1
{
	[URL("dotnet:local:namespaceName.className#methodName")]
	Integer 
	test
	()
	;
	
	String 
	test
	;
	
	Integer 
	test
	;
	
	[URL("dotnet:local:namespaceName.className#methodName")]
	String 
	test
	()
	;
}

class test3 : test2
{
	Integer 
	test
	;
	
	[URL("dotnet:local:namespaceName.className#methodName")]
	String 
	test
	()
	;
	
	[URL("dotnet:local:namespaceName.className#methodName")]
	Integer 
	test
	(String a)
	;
	
	String 
	test
	;
}