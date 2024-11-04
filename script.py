import cv2
import numpy as np
import pyautogui
import time
import os

# Set recording parameters
fps = 20       # Frames per second
screen_size = pyautogui.size()
output_filename = "screen_recording.avi"
check_file = "check.txt"  # Path to the check file

# Set up the codec and create a VideoWriter object
fourcc = cv2.VideoWriter_fourcc(*"XVID")
out = cv2.VideoWriter(output_filename, fourcc, fps, screen_size)

print("Recording... Modify check.txt to '0' to stop.")

try:
    # Start recording until check.txt is modified to contain "0"
    while True:
        # Check if check.txt contains "0"
        if os.path.exists(check_file):
            with open(check_file, "r") as f:
                if f.read().strip() == "0":
                    print("\nRecording stopped by check file.")
                    break
        
        # Capture the screen
        img = pyautogui.screenshot()
        frame = np.array(img)
        
        # Convert colors from RGB to BGR (OpenCV uses BGR)
        frame = cv2.cvtColor(frame, cv2.COLOR_RGB2BGR)
        
        # Write the frame to the video file
        out.write(frame)
        
        # Add a slight delay to reduce CPU usage
        time.sleep(1 / fps)

finally:
    # Release the video writer
    out.release()
    print(f"Screen recording saved as {output_filename}")
