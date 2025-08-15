#!/bin/bash

# Output file
OUTPUT_FILE="system.txt"

# Remove the output file if it exists
rm -f "$OUTPUT_FILE"

# Use the tree command to list files, excluding patterns
# Adjust the exclude patterns as needed
tree -fi --prune -I "*.db|*.sh|*.css|wwwroot|obj|bin" | while read -r filepath; do
    # Check if the file exists and is a regular file
    if [ -f "$filepath" ]; then
        # Append the contents of the file to the output file
        cat "$filepath" >> "$OUTPUT_FILE"
        # Optionally add a separator between files for clarity
        echo -e "\n--- End of $filepath ---\n" >> "$OUTPUT_FILE"
    fi
done

echo "Contents appended to $OUTPUT_FILE"
