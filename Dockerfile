FROM node:14

# Install hardhat
RUN npm install -g hardhat

# Copy project files into the container
COPY . /app

# Change working directory to project directory
WORKDIR /app

# Install project dependencies
RUN npm install

# Install additional dependencies
RUN npm install react-router-dom@6 ipfs-http-client@56.0.1 @openzeppelin/contracts@4.5.0

# Expose app port on the container
EXPOSE 3000