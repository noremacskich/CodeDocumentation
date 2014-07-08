##@file Makefile
#	^	Installs and uninstalls the noremacDoc
#
# @auth Adam Brett | 2013/07/19
#
# @mod NoremacSkich | 2014/07/08
#
# @src http://goo.gl/ij9kPR
#

#This is the installation directory
BIN_DIR   = /usr/bin

# This is what we want to make executable
LOADER    = git-noremacDoc

# These are the script files we want to install along with it
COMMANDS  = git-noremacDoc-hello

# This is what it would look like to have multiple files
# COMMANDS += git-noremacDoc-command

# This is a catch all area, in our case, it will show
# how to use this Makefile
all:
	@echo "usage:"
	@echo "    make [install|uninstall}"

# These are the commands that are run when they type make install
install:
	# Make sure install directory exists and has proper permissions
	install -d -m 0755 $(BIN_DIR)
	# Copy the loader file to the bin directory and make it executable
	install $(LOADER) $(BIN_DIR)
	# Copy sub command files to bin directory
	install $(COMMANDS) $(BIN_DIR)

# These are the commands that are run when they type make uninstall
uninstall:
	# Make sure directory exists
	# && \ will run all commands on single line, so if one fails, the others
	# won't be executed.
	#
	# Move to directory
	# remove files related to noremacDoc
	test -d $(BIN_DIR) && \
	cd $(BIN_DIR) && \
	rm -f $(LOADER) $(COMMANDS)


