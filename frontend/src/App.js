import React from "react";
import { ThemeProvider } from "@material-ui/styles";
import Dashboard from "./Dashboard";
import theme from "./Theme/theme";
import "./App.css";

function App() {
  return (
    <ThemeProvider theme={theme}>
      <div className="App">
        <Dashboard />
      </div>
    </ThemeProvider>
  );
}

export default App;
