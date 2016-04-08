using System;
using System.Collections.Generic;

namespace libcdx
{
    /** <p>
     * A Preference instance is a hash map holding different values. It is stored alongside your application (SharedPreferences on
     * Android, LocalStorage on GWT, on the desktop a Java Preferences file in a ".prefs" directory will be created, and on iOS an
     * NSMutableDictionary will be written to the given file). CAUTION: On the desktop platform, all libgdx applications share the same
     * ".prefs" directory. To avoid collisions use specific names like "com.myname.game1.settings" instead of "settings"
     * </p>
     * 
     * <p>
     * Changes to a preferences instance will be cached in memory until {@link #flush()} is invoked.
     * </p>
     * 
     * <p>
     * Use {@link Application#getPreferences(String)} to look up a specific preferences instance. Note that on several backends the
     * preferences name will be used as the filename, so make sure the name is valid for a filename.
     * </p>
     * 
     * @author mzechner */
    public interface IPreferences
    {
        IPreferences PutBoolean(string key, bool val);

        IPreferences PutInteger(string key, int val);

        IPreferences PutLong(string key, long val);

        IPreferences PutFloat(string key, float val);

        IPreferences PutString(string key, string val);

        IPreferences Put(Dictionary<string, object> vals);

        bool GetBoolean(string key);

        int GetInteger(string key);

        long GetLong(string key);

        float GetFloat(string key);

        string GetString(string key);

        bool GetBoolean(string key, bool defValue);

        int GetInteger(string key, int defValue);

        long GetLong(string key, long defValue);

        float GetFloat(string key, float defValue);

        string GetString(string key, string defValue);

        /** Returns a read only Map<String, Object> with all the key, objects of the preferences. */
        Dictionary<string, object> Get();

        bool Contains(string key);

        void Clear();

        void Remove(string key);

        /** Makes sure the preferences are persisted. */
        void Flush();
    }
}