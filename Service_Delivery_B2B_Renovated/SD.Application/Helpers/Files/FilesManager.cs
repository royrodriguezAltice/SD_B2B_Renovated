using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SD.Application.Common.Exceptions;
using SD.Application.Interfaces.Helpers.Files;
using SD.Domain.Common;
using SD.Domain.Entities.Adm_Presupuestos.Requisiciones.Requisicion;
using SD.Domain.Entities.Provisioning.Control_OC.Oc;
using SD.Domain.Enums.Common.Files;
using SD.Domain.Interfaces.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Helpers.Files
{
    public class FilesManager : IFilesManager
    {
        private readonly IGenericRepository<TbAnexo> _tbAnexoRepository;
        private readonly IHostingEnvironment _hostingEnviroment;

        public FilesManager(IGenericRepository<TbAnexo> tbAnexoRepository, IHostingEnvironment hostingEnviroment)
        {
            _tbAnexoRepository = tbAnexoRepository;
            _hostingEnviroment = hostingEnviroment;
        }

        public async Task<bool> FileSaving(List<IFormFile> files, string id, FilesTypes fileType)
        {
            try
            {
                //Manejar los archivos
                long filesCount = files.Count; //long filesCount = files.Sum(f => f.Length);
                int cnt = 0; //Inicializar contador
                string relativePath = ""; //Inicializando variable de ruta relativa

                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        //Obtener el nombre del archivo
                        string nombreArchivo = Path.GetFileName(file.FileName);

                        //Llamar a la funcion para generar el nombre del archivo
                        string? fileName = FilesNaming(file.FileName, id, file.ContentType, fileType);

                        //Crear la ruta relativa de donde se va a guardar el archivo
                        switch (fileType)
                        {
                            case FilesTypes.REQUISICION:
                                relativePath = Path.Combine(@"wwwroot/Files", fileName);
                                break;

                            case FilesTypes.CONDUCE:
                                relativePath = Path.Combine(@"wwwroot/Files", fileName);
                                break;

                            case FilesTypes.APN_FILE:
                                relativePath = Path.Combine(@"wwwroot/Files/Archivos_Oc_APN", fileName);
                                break;
                        }

                        //Crear la ruta completa de donde se va a guardar el archivo
                        string destinationRoute = Path.Combine(_hostingEnviroment.ContentRootPath, relativePath);

                        //Obtenerla ruta completa del archivo
                        string destinationFolder = Path.GetDirectoryName(destinationRoute);

                        //Si la carpeta no existe creela
                        if (!Directory.Exists(destinationFolder))
                        {
                            Directory.CreateDirectory(destinationFolder);
                        }

                        //Crear instancia de la tabla donde se va a guardar el archivo 
                        TbAnexo tbAnexo = new();

                        //Asignar valores a la instancia
                        tbAnexo.Identificador = id;
                        tbAnexo.NombreArchivo = fileName;

                        //Crear el archivo en la ruta de la carpeta y guardarlo en la BD
                        using (var stream = new FileStream(destinationRoute, FileMode.Create))
                        {
                            //Guardar el archivo en la ruta
                            await file.CopyToAsync(stream);

                            //Agregar datos de la requisicion
                            await _tbAnexoRepository.CreateAsync(tbAnexo);

                            cnt++;

                            if (cnt == filesCount)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new SaveFileFailedException(ex.Message);
            }
        }

        private static string FilesNaming(string fileName, string id, string contentType, FilesTypes fileType)
        {
            try
            {
                string nombreArchivoConven = "No valido";

                //Definir la convencion y la extension del archivo en base a su tipo de archivo
                switch (contentType)
                {
                    case "message/rfc822":

                        if (fileName.Contains(".eml"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                            $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".eml", string.Empty)} - {id}.eml";
                        }
                        else if (fileName.Contains(".mht"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                            $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".mht", string.Empty)} - {id}.mht";
                        }
                        else if (fileName.Contains(".mhtml"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                            $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".mhtml", string.Empty)} - {id}.mhtml";
                        }
                        else if (fileName.Contains(".mime"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                            $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".mime", string.Empty)} - {id}.mime";
                        }
                        else if (fileName.Contains(".nws"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                            $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".nws", string.Empty)} - {id}.nws";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                    case "multipart/related":

                        if (fileName.Contains(".mht"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                            $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".mht", string.Empty)} - {id}.mht";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        }

                        break;
                    case "application/octet-stream":

                        if (fileName.Contains(".tsv"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                            $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".tsv", string.Empty)} - {id}.tsv";
                        }
                        else if (fileName.Contains(".oft"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".oft", string.Empty)} - {id}.oft";
                        }
                        else if (fileName.Contains(".msg"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".msg", string.Empty)} - {id}.msg";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                    case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":

                        if (fileName.Contains(".xlsx"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".xlsx", string.Empty)} - {id}.xlsx";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                    case "application/vnd.oasis.opendocument.spreadsheet":

                        if (fileName.Contains(".ods"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".ods", string.Empty)} - {id}.ods";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                    case "application/vnd.ms-excel":

                        if (fileName.Contains(".xls"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".xls", string.Empty)} - {id}.xls";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                    case "application/msword":

                        if (fileName.Contains(".doc"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".doc", string.Empty)} - {id}.doc";
                        }
                        else if (fileName.Contains(".DOC"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".DOC", string.Empty)} - {id}.doc";
                        }
                        else if (fileName.Contains(".dot"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".dot", string.Empty)} - {id}.dot";
                        }
                        else if (fileName.Contains(".rtf"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".rtf", string.Empty)} - {id}.rtf";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                    case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":

                        if (fileName.Contains(".docx"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".docx", string.Empty)} - {id}.docx";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                    case "application/vnd.oasis.opendocument.text":

                        if (fileName.Contains(".odt"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".odt", string.Empty)} - {id}.odt";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                    case "application/rtf":

                        if (fileName.Contains(".rtf"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".rtf", string.Empty)} - {id}.rtf";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                    case "application/vnd.openxmlformats-officedocument.wordprocessingml.template":

                        if (fileName.Contains(".dotx"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".dotx", string.Empty)} - {id}.dotx";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                    case "application/pdf":

                        if (fileName.Contains(".pdf"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".pdf", string.Empty)} - {id}.pdf";
                        }
                        else if (fileName.Contains(".PDF"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".PDF", string.Empty)} - {id}.pdf";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                    case "image/jpeg":

                        if (fileName.Contains(".jpeg"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".jpeg", string.Empty)} - {id}.jpeg";
                        }
                        else if (fileName.Contains(".jpg"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".jpg", string.Empty)} - {id}.jpg";
                        }
                        else if (fileName.Contains(".pjpg"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".pjpg", string.Empty)} - {id}.pjpg";
                        }
                        else if (fileName.Contains(".jfif"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".jfif", string.Empty)} - {id}.jfif";
                        }
                        else if (fileName.Contains(".jif"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".jif", string.Empty)} - {id}.jif";
                        }
                        else if (fileName.Contains(".jpe"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".jpe", string.Empty)} - {id}.jpe";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                    case "image/png":

                        if (fileName.Contains(".png"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".png", string.Empty)} - {id}.png";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                    case "image/webp":

                        if (fileName.Contains(".webp"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".webp", string.Empty)} - {id}.webp";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                    case "text/csv":

                        if (fileName.Contains(".csv"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".csv", string.Empty)} - {id}.csv";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                    case "text/plain":

                        if (fileName.Contains(".txt"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".txt", string.Empty)} - {id}.txt";
                        }
                        else if (fileName.Contains(".text"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".text", string.Empty)} - {id}.text";
                        }
                        else if (fileName.Contains(".conf"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".conf", string.Empty)} - {id}.conf";
                        }
                        else if (fileName.Contains(".log"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".log", string.Empty)} - {id}.log";
                        }
                        else if (fileName.Contains(".tmp"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".tmp", string.Empty)} - {id}.tmp";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                    case "text/tab-separated-values":

                        if (fileName.Contains(".tsv"))
                        {
                            nombreArchivoConven = $"[{DateOnly.FromDateTime(DateTime.Now).ToString("M-dd-yyyy")}] - " +
                                $"{DateTime.Now.ToString("HH.mm tt")} - {fileType} " +
                                $"{Path.GetFileName(fileName).Replace(".tsv", string.Empty)} - {id}.tsv";
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No se puede reconocer la extension del archivo");
                            Console.WriteLine("");
                        };

                        break;
                }

                return nombreArchivoConven;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }
    }
}
