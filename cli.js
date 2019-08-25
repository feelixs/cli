#!/usr/bin/env node

'use strict';

const { spawn } = require('child_process');

spawn('dotnet', ['./Windows/CLI/bin/Debug/netcoreapp2.0/SSoTme.OST.CLI.dll', process.argv.slice(2).join(' ')], { stdio: 'inherit' });