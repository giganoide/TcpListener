# TcpListener

The service listens on the registered tcp port and writes what has been received in a file.

Run *TcpListener.exe* the console application will start.

To install the service run command *Install TcpListener.exe*.

The configuration is in the TcpListener.config file:
- *Port* is the port on which the service listens
- *FilePath* is the path of the file written


To uninstall the service: *sc delete PowerTcpListener*
