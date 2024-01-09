using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public class DocumentsFileDB
    {

        public static Guid SaveFileInDocumentsFile(byte[] FileBytes, string Extension)
        {
            Guid guid;
            using (TransactionScope DocumentsFileTransactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                using (MainDataContext context = new MainDataContext())
                {
                    context.CommandTimeout = 600;
                    long sequensFileName = context.GetMaxFileName() ?? 0;
                    sequensFileName++;

                    guid = context.ExecuteQuery<Guid>(@" INSERT INTO [dbo].[DocumentsFile]
                                                  ([name],[file_stream]) OUTPUT INSERTED.[stream_id]
                                                  SELECT {0},{1} ", sequensFileName.ToString() + Extension, FileBytes).SingleOrDefault();
                }

                DocumentsFileTransactionScope.Complete();
            }

            return guid;
        }

        public static int UpdateFileInDocumentsFile(Guid id, byte[] FileBytes)
        {
            int result;
            using (TransactionScope DocumentsFileTransactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                using (MainDataContext context = new MainDataContext())
                {
                    result = context.ExecuteCommand(@" Update [dbo].[DocumentsFile] set [file_stream] = {1} Where [stream_id] ={0} ", id, FileBytes);
                }

                DocumentsFileTransactionScope.Complete();
            }

            return result;
        }

        //        public static Guid SaveFileInFormFile(byte[] FileBytes, string Extension,long RequestID, int FormID)
        //        {
        //            Guid guid;
        //            using (TransactionScope DocumentsFileTransactionScope = new TransactionScope(TransactionScopeOption.Required))
        //            {
        //                using (MainDataContext context = new MainDataContext())
        //                {
        //                    long sequensFileName = context.GetMaxFileName() ?? 0;
        //                    sequensFileName++;

        //                    context.ExecuteQuery<Guid> ( @" INSERT INTO [dbo].[RequestForm] 
        //                                                ([RequestID] ,[FormID],[name],[file_stream]) VALUES"
        //                                                        (RequestID,FormID,sequensFileName.ToString() + Extension, FileBytes));

        //                }
        //                DocumentsFileTransactionScope.Complete();
        //            }

        //            return guid;
        //        }
        public static Guid SaveFileInDocumentsFileWithFilePathOnClinet(string path)
        {
            Guid guid;
            using (TransactionScope DocumentsFileTransactionScope = new TransactionScope(TransactionScopeOption.Required))
            {

                string extension = Path.GetExtension(path);
                byte[] fileBytes = File.ReadAllBytes(path);
                using (MainDataContext context = new MainDataContext())
                {
                    long sequensFileName = context.GetMaxFileName() ?? 0;
                    sequensFileName++;

                    guid = context.ExecuteQuery<Guid>(@" INSERT INTO [dbo].[DocumentsFile]
                                                  ([name],[file_stream]) OUTPUT INSERTED.[stream_id]
                                                  SELECT {0},{1} ", sequensFileName.ToString() + extension, fileBytes).SingleOrDefault();
                }

                DocumentsFileTransactionScope.Complete();
            }

            return guid;
        }
        public static Guid SaveFileInDocumentsFileWithFilePathOnServer(string path)
        {

            // i do not know that Path.GetExtension(path) get path on server

            Guid guid;
            using (TransactionScope DocumentsFileTransactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                using (MainDataContext context = new MainDataContext())
                {
                    long sequensFileName = context.GetMaxFileName() ?? 0;
                    sequensFileName++;
                    guid = context.ExecuteQuery<Guid>(@"Insert into DocumentsFile([name],[file_stream])  OUTPUT INSERTED.[stream_id] SELECT '" + sequensFileName.ToString() + Path.GetExtension(path) + "', *  FROM OPENROWSET(BULK  '" + path + "', SINGLE_BLOB) AS FileData").SingleOrDefault(); ;
                }

                DocumentsFileTransactionScope.Complete();
            }

            return guid;
        }

        public static void DeleteDocumentsFileTable(Guid guid)
        {
            using (MainDataContext context = new MainDataContext())
            {
                context.ExecuteCommand(@"DELETE FROM dbo.DocumentsFile WHERE stream_id = {0} ", guid);
            }
        }

        public static FileInfo GetDocumentsFileTable(Guid guid)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExecuteQuery<FileInfo>(@"SELECT [file_stream] as Content , [file_type] as FileType FROM [dbo].DocumentsFile WHERE stream_id = {0}", guid).SingleOrDefault();
            }
        }

    }
}
