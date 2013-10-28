@rem
@rem This script uses the svcutil tool (from .NET Framework 4.0) to generate the .NET client classes for the SOLA web services using
@rem an inlined WSDL file. Generating the .NET client classes directly from the hosted web service causes the generated classes to
@rem become invalid. The cause of this problem is not clear. 
@rem
@rem The inlined WSDL files can be generated from the SOLA Boundary Web Services project by adding the <inlineSchemas>true</inlineSchemas>
@rem config option into the jaxws-maven-pluggin configuration section of the POM.xml and rebuilding that project. The inlined schemas will
@rem get created in the ...\code\services\boundary\wsdl folder. 
@rem  
@rem To ensure the correct WCF config files with the necessary security information, this script also uses the hosted
@rem services to create the appropriate WCF config file. The source code file(s) generated from the hosted services should be ignored. 
@rem
@rem Known issues:
@rem  - After generating the SearchService, you need to replace any occurances of [][] with [] (should only be 2 occurances). 
@rem  - The generated client classes need to be placed in at least 2 namespaces. The primary namespace is the main namespace for the
@rem    service (e.g. org.sola.webservices.<service name>). The default namespace for all other client classes should be 
@rem    org.sola.webservices.<service name>.extra 
@rem  - After running this script, you may encounter compile issues with the project due to missing using statements. Simply add the
@rem    necessary using statements back into the generated source code files to fix the compile issues - 
@rem    e.g using org.sola.webservices.search.extra;
@rem

set HOST=192.168.43.184:8080
set SVC_BASE=sola/webservices

"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\svcutil" /language:cs /config:Ignore.config /n:http://webservices.sola.org/search,org.sola.webservices.search /n:*,org.sola.webservices.search.extra /edb /serializer:XmlSerializer /fault /ct:System.Collections.ArrayList /out:SearchService ./wsdl/SearchService.wsdl
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\svcutil" /language:cs /config:Ignore.config /n:http://webservices.sola.org/administrative,org.sola.webservices.administrative /n:*,org.sola.webservices.administrative.extra /edb /serializer:XmlSerializer /fault /ct:System.Collections.ArrayList /out:AdministrativeService ./wsdl/AdministrativeService.wsdl
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\svcutil" /language:cs /config:Ignore.config /n:http://webservices.sola.org/casemanagement,org.sola.webservices.casemanagement /n:*,org.sola.webservices.casemanagement.extra /edb /serializer:XmlSerializer /fault /ct:System.Collections.ArrayList /out:CasemanagementService ./wsdl/CasemanagementService.wsdl
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\svcutil" /language:cs /config:Ignore.config /n:http://webservices.sola.org/filestreaming,org.sola.webservices.filestreaming /n:*,org.sola.webservices.filestreaming.extra /edb /serializer:XmlSerializer /fault /ct:System.Collections.ArrayList /out:FilestreamingService ./wsdl/FilestreamingService.wsdl
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\svcutil" /language:cs /config:Ignore.config /n:http://webservices.sola.org/referencedata,org.sola.webservices.referencedata /n:*,org.sola.webservices.referencedata.extra /edb /serializer:XmlSerializer /fault /ct:System.Collections.ArrayList /out:ReferencedataService ./wsdl/ReferencedataService.wsdl
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\svcutil" /language:cs /config:Ignore.config /n:http://webservices.sola.org/digitalarchive,org.sola.webservices.digitalarchive /n:*,org.sola.webservices.digitalarchive.extra /edb /serializer:XmlSerializer /fault /ct:System.Collections.ArrayList /out:DigitalArchiveService ./wsdl/DigitalarchiveService.wsdl


@rem Generate the correct config files based on the hosted web service
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\svcutil" /language:cs /config:SearchService.config /n:*,org.sola.webservices.search /edb /serializer:XmlSerializer /fault /ct:System.Collections.ArrayList /out:Ignore http://%HOST%/%SVC_BASE%/search-service?wsdl
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\svcutil" /language:cs /config:AdministrativeService.config /n:*,org.sola.webservices.administrative /edb /serializer:XmlSerializer /fault /ct:System.Collections.ArrayList /out:Ignore http://%HOST%/%SVC_BASE%/administrative-service?wsdl
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\svcutil" /language:cs /config:CasemanagementService.config /n:*,org.sola.webservices.casemanagement /edb /serializer:XmlSerializer /fault /ct:System.Collections.ArrayList /out:Ignore http://%HOST%/%SVC_BASE%/casemanagement-service?wsdl
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\svcutil" /language:cs /config:FilestreamingService.config /n:*,org.sola.webservices.filestreaming /edb /serializer:XmlSerializer /fault /ct:System.Collections.ArrayList /out:Ignore http://%HOST%/%SVC_BASE%/filestreaming-service?wsdl
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\svcutil" /language:cs /config:ReferencedataService.config /n:*,org.sola.webservices.referencedata /edb /serializer:XmlSerializer /fault /ct:System.Collections.ArrayList /out:Ignore http://%HOST%/%SVC_BASE%/referencedata-service?wsdl
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\svcutil" /language:cs /config:DigitalArchiveService.config /n:*,org.sola.webservices.digitalarchive /edb /serializer:XmlSerializer /fault /ct:System.Collections.ArrayList /out:Ignore http://%HOST%/%SVC_BASE%/digitalarchive-service?wsdl
pause