package org.webappos.adapters.webcalls.lua;

import java.lang.reflect.Method;

import org.luaj.vm2.LuaTable;
import org.luaj.vm2.LuaValue;
import org.luaj.vm2.Varargs;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.webappos.server.API;
import org.webappos.util.StackTrace;

import lv.lumii.tda.raapi.RAAPI;

import org.luaj.vm2.lib.LibFunction;
import org.luaj.vm2.lib.OneArgFunction;
import org.luaj.vm2.lib.ThreeArgFunction;
import org.luaj.vm2.lib.TwoArgFunction;
import org.luaj.vm2.lib.VarArgFunction;

public class Module_lua_java extends TwoArgFunction {
	
	private static Logger logger =  LoggerFactory.getLogger(Module_lua_java.class);
	
	public static Module_lua_java LIB = null;
	private RAAPI raapi = null;
	private String appFullName;
	
    public Module_lua_java(RAAPI _raapi, String _appFullName) {
        LIB = this;
        raapi = _raapi;
        appFullName = _appFullName;
    }

	@Override
	public LuaValue call(LuaValue modName, LuaValue env) {
		LuaTable module = new LuaTable(0,30); // I think "new LuaTable()" instead of "(0, 30)" is OK
        module.set("call_java_through_pipe", new call_java_through_pipe());
        module.set("call_static_class_method", new call_static_class_method());
        module.set("java_pipe_close", new java_pipe_close());
        return module;		
	}

	abstract public class FiveArgFunction extends LibFunction {

		abstract public LuaValue call(LuaValue arg1, LuaValue arg2, LuaValue arg3, LuaValue arg4, LuaValue arg5);

		public FiveArgFunction() {
		}

		public FiveArgFunction(LuaValue env) {
			this .env = env;
		}

		@Override
		public final LuaValue call() {
		return call(NIL, NIL, NIL, NIL, NIL);
		}

		@Override
		public final LuaValue call(LuaValue arg) {
		return call(arg, NIL, NIL, NIL, NIL);
		}

		@Override
		public LuaValue call(LuaValue arg1, LuaValue arg2) {
		return call(arg1, arg2, NIL, NIL, NIL);
		}

		@Override
		public LuaValue call(LuaValue arg1, LuaValue arg2, LuaValue arg3) {
		return call(arg1, arg2, arg3, NIL, NIL);
		}

		public LuaValue call(LuaValue arg1, LuaValue arg2, LuaValue arg3, LuaValue arg4) {
		return call(arg1, arg2, arg3, arg4, NIL);
		}

		@Override
		public Varargs invoke(Varargs varargs) {
			return call(varargs.arg1(), varargs.arg(2), varargs.arg(3), varargs.arg(4), varargs.arg(5));
		}

	}	
	
	// Usage from Lua:
	// local java_pipe_handle_address, value, err = lua_java.call_java_through_pipe(java_pipe_handle_address, jvm_options, class_name, public_static_method_name, string_arg)
	
	public class call_java_through_pipe extends VarArgFunction {	
		
		public Varargs invoke(Varargs v) {

			String retVal = "";
			LuaValue err = LuaValue.NIL;
			
			String className = v.arg(3).tojstring();
			String methodName = v.arg(4).tojstring();
			String stringArg = v.arg(5).tojstring();
			
			
			try {				
				Class<?> c = null;
				try {
					c = Class.forName(className);
				}
				catch(Throwable t) {
					API.classLoader.addClasspathsForPropertiesId(appFullName);
					c = API.classLoader.findClassByName(className);
				}
				Method  method = c.getDeclaredMethod (methodName, String.class);
				retVal = (method.invoke(null, stringArg)).toString();
			} catch (Throwable t) {
				
				err = LuaValue.valueOf(t.toString());
				
				logger.error("call_java_through_pipe exception "+t.getMessage()+"\nStackTrace="+StackTrace.get(t));
			}
			
			
		    Varargs varargs = LuaValue.varargsOf(new LuaValue[] {
		    		LuaValue.valueOf(0), // java pipe handle		    		
		    		LuaValue.valueOf(retVal), 
		            err});

		    return varargs;

		}

			
/*
 * 		// send class name, method name and method arg through pipe for execution
		TDA_WriteProcessInputStream(process_handle, (void*)class_name, strlen(class_name)+1, sent);
		TDA_WriteProcessInputStream(process_handle, (void*)method_name, strlen(method_name)+1, sent);
		TDA_WriteProcessInputStream(process_handle, (void*)string_arg, strlen(string_arg)+1, sent);

		// read result string back
		char* status = read_from_pipe(process_handle);

		char* result_or_err = read_from_pipe(process_handle);		

		// return results to lua
		if (strcmp(status, "error") == 0) {
			lua_pushnumber(L, (long)process_handle); // the same handle adress, because pipe still usable
			lua_pushnil(L); // no result
			lua_pushstring(L, result_or_err); // error message
		} else if (strcmp(status, "terminal_error") == 0){
			TDA_ReleasePipedProcess(process_handle, false);

			lua_pushnumber(L, 0); // 0, because pipe loop broken
			lua_pushnil(L);	// no result
			lua_pushstring(L, result_or_err); // error message
		} else {
			lua_pushnumber(L, (long)process_handle); // the same handle adress, because pipe still usable
			lua_pushstring(L, result_or_err); // result str
			lua_pushnil(L); // no error
		}
					
 */

	}
	
	
	public class call_static_class_method extends ThreeArgFunction {

		@Override
		public LuaValue call(LuaValue className, LuaValue methodName, LuaValue stringArg) {
			
			String retVal = "";
			
			try {
				Class<?> c = Class.forName(className.tojstring());
				Method  method = c.getDeclaredMethod (methodName.tojstring(), String.class);
				retVal = (String)(method.invoke(null, stringArg.tojstring()));
			} catch (Throwable t) {
				logger.error("call_static_class_method exception "+t.getMessage()+"\nStackTrace="+StackTrace.get(t));
				retVal = "error - "+t.toString();
			}

			return LuaValue.valueOf(retVal);
		}		


	}
	
	// Usage from Lua:
	// lua_java.java_pipe_close(java_pipe_handle_address)
	
	public class java_pipe_close extends OneArgFunction {		

		@Override
		public LuaValue call(LuaValue arg) {
			//.java_pipe_close(java_pipe_handle_address)
			// TODO Auto-generated method stub
			return null;
		}

	}
	

}
