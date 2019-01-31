package org.webappos.webproc;

public interface IWebProcessorAdapter {
	/**
	 * Launches (local) or connects (remote) a web processor.
	 * 
	 * The adapter must register the underlying web processor by calling registerWebProcessor within Web Processor Bus Service.
	 *  
	 * @param location the adapter-specific location of the web processor code; for the "local" adapter, contains a Java class name;
	 * for the "remote" adapter contains a URL
	 */
	public void connect(String location, String id);		
	public void disconnect(IRWebProcessor wpAPI); // on shutdown or by request; must terminate the current execution
}
