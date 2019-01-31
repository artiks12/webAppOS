// automatically generated
package lv.lumii.tda.webde.mm; 

import java.util.*;
import lv.lumii.tda.raapi.RAAPI;

public class D_SHARP_RichTextArea
	extends D_SHARP_TextArea
  	implements RAAPIReferenceWrapper
{
	/* these references are defined only in the top-most superclass:
	protected DialogEngineMetamodelFactory factory;
	protected long rObject = 0;
	protected boolean takeReference;
	*/

	public RAAPI getRAAPI()
	{
		return factory.raapi;
	}
	public long getRAAPIReference()
	{
		return rObject;
	}

	public boolean delete()
	{
		if (rObject != 0) {
			if (!takeReference) {
				System.err.println("Unable to delete the object "+rObject+" of type D_SHARP_RichTextArea since the RAAPI wrapper does not take care of this reference.");
				return false;
			}
			factory.wrappers.remove(rObject);
			boolean retVal = factory.raapi.deleteObject(rObject);
			if (retVal) {
				rObject = 0;
			}
			else
				factory.wrappers.put(rObject, this); // putting back
			return retVal;
		}
		else
			return false;
	}

	public void finalize()
	{
		if (rObject != 0) {
			if (takeReference) {
				factory.wrappers.remove(rObject);
				factory.raapi.freeReference(rObject);
			}
			rObject = 0;
		}
	}


	// package-visibility:
	D_SHARP_RichTextArea(DialogEngineMetamodelFactory _factory)
	{
		super(_factory, _factory.raapi.createObject(_factory.D_SHARP_RICHTEXTAREA), true);		
		factory = _factory;
		rObject = super.rObject;
		takeReference = true;
		factory.wrappers.put(rObject, this);
		/*
		factory = _factory;
		rObject = factory.raapi.createObject(factory.D_SHARP_RICHTEXTAREA);			
		takeReference = true;
		factory.wrappers.put(rObject, this);
		*/
	}

	public D_SHARP_RichTextArea(DialogEngineMetamodelFactory _factory, long _rObject, boolean _takeReference)
	{
		super(_factory, _rObject, _takeReference);
		/*
		factory = _factory;
		rObject = _rObject;
		takeReference = _takeReference;
		if (takeReference)
			factory.wrappers.put(rObject, this);
		*/
	}

	// iterator for instances...
	public static Iterable<? extends D_SHARP_RichTextArea> allObjects()
	{
		return allObjects(DialogEngineMetamodelFactory.eINSTANCE);
	} 

	public static Iterable<? extends D_SHARP_RichTextArea> allObjects(DialogEngineMetamodelFactory factory)
	{
		ArrayList<D_SHARP_RichTextArea> retVal = new ArrayList<D_SHARP_RichTextArea>();
		long it = factory.raapi.getIteratorForAllClassObjects(factory.D_SHARP_RICHTEXTAREA);
		if (it == 0)
			return retVal;
		long r = factory.raapi.resolveIteratorFirst(it);
		while (r != 0) {
 			D_SHARP_RichTextArea o = (D_SHARP_RichTextArea)factory.findOrCreateRAAPIReferenceWrapper(r, true);
			if (o == null)
				o = (D_SHARP_RichTextArea)factory.findOrCreateRAAPIReferenceWrapper(D_SHARP_RichTextArea.class, r, true);
			if (o != null)
				retVal.add(o);
			r = factory.raapi.resolveIteratorNext(it);
		}
		factory.raapi.freeIterator(it);
		return retVal;
	}

	public static boolean deleteAllObjects()
	{
		return deleteAllObjects(DialogEngineMetamodelFactory.eINSTANCE);
	}

	public static boolean deleteAllObjects(DialogEngineMetamodelFactory factory)
	{
		for (D_SHARP_RichTextArea o : allObjects(factory))
			o.delete();
		return firstObject(factory) == null;
	}

	public static D_SHARP_RichTextArea firstObject()
	{
		return firstObject(DialogEngineMetamodelFactory.eINSTANCE);
	} 

	public static D_SHARP_RichTextArea firstObject(DialogEngineMetamodelFactory factory)
	{
		long it = factory.raapi.getIteratorForAllClassObjects(factory.D_SHARP_RICHTEXTAREA);
		if (it == 0)
			return null;
		long r = factory.raapi.resolveIteratorFirst(it);
		factory.raapi.freeIterator(it);
		if (r == 0)
			return null;
		else {
			D_SHARP_RichTextArea  retVal = (D_SHARP_RichTextArea)factory.findOrCreateRAAPIReferenceWrapper(r, true);
			if (retVal == null)
				retVal = (D_SHARP_RichTextArea)factory.findOrCreateRAAPIReferenceWrapper(D_SHARP_RichTextArea.class, r, true);
			return retVal;
		}
	} 
 
	public String getFileName()
	{
		return factory.raapi.getAttributeValue(rObject, factory.D_SHARP_RICHTEXTAREA_FILENAME);
	}
	public boolean setFileName(String value)
	{
		if (value == null)
			return factory.raapi.deleteAttributeValue(rObject, factory.D_SHARP_RICHTEXTAREA_FILENAME);
		return factory.raapi.setAttributeValue(rObject, factory.D_SHARP_RICHTEXTAREA_FILENAME, value.toString());
	}
	public String getEncodedContent()
	{
		return factory.raapi.getAttributeValue(rObject, factory.D_SHARP_RICHTEXTAREA_ENCODEDCONTENT);
	}
	public boolean setEncodedContent(String value)
	{
		if (value == null)
			return factory.raapi.deleteAttributeValue(rObject, factory.D_SHARP_RICHTEXTAREA_ENCODEDCONTENT);
		return factory.raapi.setAttributeValue(rObject, factory.D_SHARP_RICHTEXTAREA_ENCODEDCONTENT, value.toString());
	}
}
