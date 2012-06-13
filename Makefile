LIBSOURCES=src/AppConfigHandler.cs src/CmdLineHandler.cs src/CollectionExtensions.cs src/KeyValueHandler.cs src/NullHandler.cs src/SystemConfiguration.cs src/SystemEnvironmentHandler.cs src/UniversalConfigAttribute.cs src/UniversalConfiguration.cs

TESTSOURCES= src/MyConfig.cs src/TestAppConfigHandler.cs src/TestCmdLineHandler.cs src/TestConfiguration.cs src/TestSysEnvHandler.cs src/TestExtensions.cs

CONFIGSRC=src/app.config
CONFIG=poros.test.dll.config

LIBS=..

OUTDIR=..

.PHONY: all
all: $(OUTDIR)/poros.dll

.PHONY: tests
tests: xunit.dll poros.dll Poros.Test.dll
	xunit Poros.Test.dll

xunit.dll:
	cp $(LIBS)/xunit.dll .

$(OUTDIR)/poros.dll: tests poros.dll
	cp poros.dll $(OUTDIR)

poros.dll: $(LIBSOURCES)
	dmcs -r:System.Configuration.dll -t:library -out:poros.dll $(LIBSOURCES)

Poros.Test.dll: poros.dll $(TESTSOURCES) $(CONFIG)
	dmcs -lib:$(LIBS) -r:poros.dll -r:System.Configuration.dll -r:xunit.dll -t:library -out:Poros.Test.dll $(TESTSOURCES)

$(CONFIG) : $(CONFIGSRC)
	cp $(CONFIGSRC) $(CONFIG)
