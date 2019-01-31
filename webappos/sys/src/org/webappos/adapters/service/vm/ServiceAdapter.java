package org.webappos.adapters.service.vm;

import org.eclipse.jetty.server.handler.ContextHandler;
import org.webappos.properties.ServiceProperties;
import org.webappos.server.API;
import org.webappos.server.IServiceAdapter;

public class ServiceAdapter implements IServiceAdapter {

	private QEMULauncher qemu = null;
	@Override
	public ContextHandler attachService(ServiceProperties svcProps, String path, Runnable onStopped, Runnable onHalted) {
		qemu = new QEMULauncher(API.config.properties.getProperty("qemu_path"), svcProps);
		if (qemu.launch(onStopped, onHalted))
			return null;
		else {
			qemu = null;
			throw new RuntimeException("Could not launch qemu for service "+svcProps.service_full_name);
		}
	}

	@Override
	public void stopService() {
		if (qemu!=null) {
			qemu.stop();
			qemu = null;
		}
	}

}
