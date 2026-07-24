using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Unity.XGamingRuntime.Interop
{
    internal static partial class XblInterop
    {
#if (!UNITY_STANDALONE_WIN && ENABLE_IL2CPP)
        public const string XblThunkDllName = "__Internal";
#elif UNITY_GDK_MOCK_XBL
        public const string XblThunkDllName = "Microsoft.Xbox.Services.GDK.C.Thunks.Mock";
#else
        public const string XblThunkDllName = "Microsoft.Xbox.Services.GDK.C.Thunks";
#endif
        
        #region Xbox live size constants from types_c.h
        //#define XBL_COLOR_CHAR_SIZE                     (7 * 3)
        internal const Int32 XBL_COLOR_CHAR_SIZE = (7 * 3);

        //#define XBL_DISPLAY_NAME_CHAR_SIZE              (30 * 3)
        internal const Int32 XBL_DISPLAY_NAME_CHAR_SIZE = (30 * 3);

        //#define XBL_DISPLAY_PIC_URL_RAW_CHAR_SIZE       (225 * 3)
        internal const Int32 XBL_DISPLAY_PIC_URL_RAW_CHAR_SIZE = (225 * 3);

        //#define XBL_GAMERSCORE_CHAR_SIZE                (16 * 3)
        internal const Int32 XBL_GAMERSCORE_CHAR_SIZE = (16 * 3);

        //#define XBL_GAMERTAG_CHAR_SIZE                  (16 * 3)
        internal const Int32 XBL_GAMERTAG_CHAR_SIZE = (16 * 3);

        //#define XBL_MODERN_GAMERTAG_CHAR_SIZE           (((12 + 12) * 4) + 1) // 12 characters + 12 diacritic, 4 bytes each, plus 1 byte null terminator
        internal const Int32 XBL_MODERN_GAMERTAG_CHAR_SIZE = (((12 + 12) * 4) + 1);

        //#define XBL_MODERN_GAMERTAG_SUFFIX_CHAR_SIZE    (14 + 1) // 14 alphanumeric characters + null terminator
        internal const Int32 XBL_MODERN_GAMERTAG_SUFFIX_CHAR_SIZE = (14 + 1);

        //#define XBL_UNIQUE_MODERN_GAMERTAG_CHAR_SIZE    (XBL_MODERN_GAMERTAG_CHAR_SIZE + 1 + 3 ) // modern gamertag + '#' + max suffix size for cases when MGT display length is 12. Null terminator already accoutned for in MGT
        internal const Int32 XBL_UNIQUE_MODERN_GAMERTAG_CHAR_SIZE = (XBL_MODERN_GAMERTAG_CHAR_SIZE + 1 + 3);

        //#define XBL_NUM_PRESENCE_RECORDS 6
        internal const Int32 XBL_NUM_PRESENCE_RECORDS = 6;

        //#define XBL_REAL_NAME_CHAR_SIZE                 (255 * 3)
        internal const Int32 XBL_REAL_NAME_CHAR_SIZE = (255 * 3);

        //#define XBL_RICH_PRESENCE_CHAR_SIZE             (100 * 3)
        internal const Int32 XBL_RICH_PRESENCE_CHAR_SIZE = (100 * 3);

        //#define XBL_TITLE_NAME_CHAR_SIZE
        internal const Int32 XBL_TITLE_NAME_CHAR_SIZE = (100 * 3);

        //#define XBL_XBOX_USER_ID_CHAR_SIZE              (21 * 3)
        internal const Int32 XBL_XBOX_USER_ID_CHAR_SIZE = (21 * 3);

        //#define XBL_GUID_LENGTH                         40
        internal const Int32 XBL_GUID_LENGTH = 40;

        //#define XBL_SCID_LENGTH                         XBL_GUID_LENGTH
        internal const Int32 XBL_SCID_LENGTH = XBL_GUID_LENGTH;

        //#define XBL_SOCIAL_MANAGER_MAX_AFFECTED_USERS_PER_EVENT 10
        internal const Int32 XBL_SOCIAL_MANAGER_MAX_AFFECTED_USERS_PER_EVENT = 10;

        internal const Int32 XBL_MULTIPLAYER_SESSION_TEMPLATE_NAME_MAX_LENGTH = 100;

        internal const Int32 XBL_MULTIPLAYER_SESSION_NAME_MAX_LENGTH = XBL_MULTIPLAYER_SESSION_TEMPLATE_NAME_MAX_LENGTH;

        internal const Int32 XBL_MULTIPLAYER_SESSION_REFERENCE_URI_MAX_LENGTH = (44 + XBL_SCID_LENGTH + XBL_MULTIPLAYER_SESSION_TEMPLATE_NAME_MAX_LENGTH + XBL_MULTIPLAYER_SESSION_NAME_MAX_LENGTH);
        #endregion

        #region Xbox live size constants from multiplayer_c.h
        // #define XBL_MULTIPLAYER_SEARCH_HANDLE_MAX_FIELD_LENGTH 100
        internal const Int32 XBL_MULTIPLAYER_SEARCH_HANDLE_MAX_FIELD_LENGTH = 100;
        #endregion

        #region Xbox live size constants from title_storage_c.h
        //#define XBL_TITLE_STORAGE_MIN_UPLOAD_BLOCK_SIZE 1024
        internal const Int32 XBL_TITLE_STORAGE_MIN_UPLOAD_BLOCK_SIZE = 1024;
        //#define XBL_TITLE_STORAGE_MAX_UPLOAD_BLOCK_SIZE (4 * 1024 * 1024)
        internal const Int32 XBL_TITLE_STORAGE_MAX_UPLOAD_BLOCK_SIZE = (4 * 1024 * 1024);
        //#define XBL_TITLE_STORAGE_DEFAULT_UPLOAD_BLOCK_SIZE (256 * 1024)
        internal const Int32 XBL_TITLE_STORAGE_DEFAULT_UPLOAD_BLOCK_SIZE = (256 * 1024);
        //#define XBL_TITLE_STORAGE_MIN_DOWNLOAD_BLOCK_SIZE 1024
        internal const Int32 XBL_TITLE_STORAGE_MIN_DOWNLOAD_BLOCK_SIZE = 1024;
        //#define XBL_TITLE_STORAGE_DEFAULT_DOWNLOAD_BLOCK_SIZE (1024 * 1024)
        internal const Int32 XBL_TITLE_STORAGE_DEFAULT_DOWNLOAD_BLOCK_SIZE = (1024 * 1024);

        //#define XBL_TITLE_STORAGE_BLOB_PATH_MAX_LENGTH (257 * 3)
        internal const Int32 XBL_TITLE_STORAGE_BLOB_PATH_MAX_LENGTH = (257 * 3);
        //#define XBL_TITLE_STORAGE_BLOB_DISPLAY_NAME_MAX_LENGTH (129 * 3)
        internal const Int32 XBL_TITLE_STORAGE_BLOB_DISPLAY_NAME_MAX_LENGTH = (129 * 3);
        //#define XBL_TITLE_STORAGE_BLOB_ETAG_MAX_LENGTH (18 * 3) // TODO confirm with Azure Blob Services team
        internal const Int32 XBL_TITLE_STORAGE_BLOB_ETAG_MAX_LENGTH = (18 * 3);
        #endregion
#if (!UNITY_STANDALONE_WIN && ENABLE_IL2CPP)
        //XBL_DLLEXPORT HRESULT XBL_CALLING_CONV XblWrapper_XblInitialize(
        //    _In_z_ const char* scid,
        //    _In_ XTaskQueueHandle internalWorkQueue
        //) noexcept
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblInitialize(XblInitArgsPtr args);
#else
        //XBL_DLLEXPORT HRESULT XBL_CALLING_CONV XblWrapper_XblInitialize(
        //    _In_z_ const char* scid,
        //    _In_ XTaskQueueHandle internalWorkQueue
        //) noexcept
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblWrapper_XblInitialize(
            Byte[] scid,
            XTaskQueueHandle internalWorkQueue);

        //XBL_DLLEXPORT HRESULT XblWrapper_XblCleanupAsync(XAsyncBlock* async) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblWrapper_XblCleanupAsync(XAsyncBlockPtr asyncBlock);
#endif

        /// <summary>
        /// Immediately reclaims all resources associated with the library.
        /// If you called XblMemSetFunctions(), call this before shutting down your app's memory manager.
        /// It is the responsibility of the game to wait for any outstanding Async calls to complete before calling XblCleanup.
        /// </summary>
        //STDAPI XblCleanupAsync(XAsyncBlock* async) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblCleanupAsync(XAsyncBlockPtr asyncBlock);
    }
}