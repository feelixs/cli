#!/usr/bin/env node

'use strict';

const { spawn, execSync } = require('child_process');
const path = require('path');
const fs = require('fs');

const appDir = path.dirname(require.main.filename);
const solutionPath = path.join(appDir, 'SSoTme-OST-CLI.sln');
const outputPath = path.join(appDir, 'Windows', 'CLI', 'bin', 'Release', 'net8.0', 'SSoTme.OST.CLI.dll');

// Check if we need to build
if (!fs.existsSync(outputPath)) {
    console.log('Building .NET solution...');
    try {
        execSync(`dotnet build "${solutionPath}" --configuration Release`, { 
            stdio: 'inherit',
            cwd: appDir
        });
    } catch (error) {
        console.error('Failed to build .NET solution:', error);
        process.exit(1);
    }
}

// Run the CLI
try {
    spawn('dotnet', [
        outputPath,
        process.argv.slice(2).join(' ')
    ], { 
        stdio: 'inherit',
        // cwd: appDir 
    });
} catch (error) {
    console.error('Failed to run CLI:', error);
    process.exit(1);
}