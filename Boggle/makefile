CC=g++
RM=rm
CFLAGS=-c -Wall
LDFLAGS=
SOURCES = AssertException.cpp \
	Test.cpp \
	Checks.cpp \
	TestRunner.cpp \
	TestResults.cpp \
	TestReporter.cpp \
	TestReporterStdout.cpp \
	ReportAssert.cpp \
	TestList.cpp \
	TimeConstraint.cpp \
	TestDetails.cpp \
	MemoryOutStream.cpp \
	DeferredTestReporter.cpp \
	DeferredTestResult.cpp \
	XmlTestReporter.cpp \
	CurrentTest.cpp \
    Boggle.cpp \
    test2.cpp

SOURCES += SignalTranslator.cpp \
	TimeHelpers.cpp

OBJECTS=$(SOURCES:.cpp=.o)
EXECUTABLE=Boggle_Test
VPATH=UnitTest++/src
VPATH+=UnitTest++/src/Posix

all: $(EXECUTABLE)

$(EXECUTABLE): $(OBJECTS)
	$(CC) $(LDFLAGS) $(OBJECTS) -o $@

.cpp.o:
	$(CC) $(CFLAGS) $< -o $@

clean:
	$(RM) -rf *o Boggle
